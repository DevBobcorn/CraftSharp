#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    /// <summary>
    /// Group tag that contains a collection of resource locations
    /// </summary>
    public sealed class GroupTag : IReadOnlyCollection<ResourceLocation>
    {
        public const string DEFAULT_NAMESPACE = "minecraft";
        private const char TAG_PREFIX = '#';

        private static readonly Dictionary<string, Dictionary<ResourceLocation, GroupTag>> tagsByType = new();

        public string Type { get; }
        public ResourceLocation TagId { get; }

        private readonly HashSet<ResourceLocation> directEntries = new();
        private readonly HashSet<ResourceLocation> tagReferences = new();
        private HashSet<ResourceLocation>? resolvedEntries;

        private GroupTag(string type, ResourceLocation tagId)
        {
            Type = type;
            TagId = tagId;
        }

        public int Count => GetResolvedEntries().Count;

        public IEnumerator<ResourceLocation> GetEnumerator()
        {
            return GetResolvedEntries().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ResourceLocation[] GetEntries()
        {
            return GetResolvedEntries().ToArray();
        }

        public static IReadOnlyDictionary<ResourceLocation, GroupTag> GetTagsByType(string type)
        {
            return tagsByType.TryGetValue(type, out var tags) ? tags : new Dictionary<ResourceLocation, GroupTag>();
        }

        public static List<GroupTag> GetTagsForObject(string type, ResourceLocation objectId)
        {
            var tagsForObject = new List<GroupTag>();
            if (tagsByType.TryGetValue(type, out var tags))
            {
                foreach (var (tagId, tag) in tags)
                {
                    if (tag.Contains(objectId))
                    {
                        tagsForObject.Add(tag);
                    }
                }
            }
            return tagsForObject;
        }

        public static bool TryGetTag(string type, ResourceLocation tagId, [NotNullWhen(true)] out GroupTag? tag)
        {
            tag = null;
            return tagsByType.TryGetValue(type, out var tags) && tags.TryGetValue(tagId, out tag);
        }

        public static bool TryGetEntries(string type, ResourceLocation tagId, out ResourceLocation[] entries)
        {
            if (TryGetTag(type, tagId, out var tag))
            {
                entries = tag.GetEntries();
                return entries.Length > 0;
            }

            entries = Array.Empty<ResourceLocation>();
            return false;
        }

        public static void Clear()
        {
            tagsByType.Clear();
        }

        /// <summary>
        /// Load group tag data from external files.
        /// </summary>
        /// <param name="flag">Data load flag</param>
        public static void PrepareData(DataLoadFlag flag)
        {
            // Clear loaded stuff...
            Clear();

            var tagsRoot = PathHelper.GetExtraDataFile("tags");

            if (!Directory.Exists(tagsRoot))
            {
                Debug.LogWarning("Tag data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                LoadFromDirectory(tagsRoot);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to load group tags: {e.Message}");
                flag.Failed = true;
            }
            finally
            {
                flag.Finished = true;
            }
        }

        public static void LoadFromDirectory(string tagsRoot)
        {
            Clear();

            if (!Directory.Exists(tagsRoot))
            {
                Debug.LogWarning($"Group tag directory not found: {tagsRoot}");
                return;
            }

            foreach (var typeDir in Directory.GetDirectories(tagsRoot))
            {
                var tagType = new DirectoryInfo(typeDir).Name;
                var tagTable = new Dictionary<ResourceLocation, GroupTag>();

                foreach (var file in Directory.GetFiles(typeDir, "*.json", SearchOption.AllDirectories))
                {
                    var tagId = BuildTagId(typeDir, file);
                    var tag = new GroupTag(tagType, tagId);
                    LoadTagFile(file, tag);
                    tagTable[tagId] = tag;
                }

                tagsByType[tagType] = tagTable;

                foreach (var tag in tagTable.Values)
                {
                    _ = tag.GetResolvedEntries();
                }
            }
        }

        private static ResourceLocation BuildTagId(string typeDir, string filePath)
        {
            var relative = Path.GetRelativePath(typeDir, filePath);
            relative = Path.ChangeExtension(relative, null);
            relative = relative.Replace(Path.DirectorySeparatorChar, '/');
            relative = relative.Replace(Path.AltDirectorySeparatorChar, '/');
            return new ResourceLocation(DEFAULT_NAMESPACE, relative);
        }

        private static void LoadTagFile(string filePath, GroupTag tag)
        {
            try
            {
                var tagJson = Json.ParseJson(File.ReadAllText(filePath, Encoding.UTF8));

                if (tagJson.Properties.TryGetValue("values", out var values))
                {
                    foreach (var entry in values.DataArray)
                    {
                        var entryStr = entry.StringValue;

                        if (string.IsNullOrWhiteSpace(entryStr) &&
                            entry.Properties.TryGetValue("id", out var idValue))
                        {
                            entryStr = idValue.StringValue;
                        }

                        if (string.IsNullOrWhiteSpace(entryStr))
                        {
                            continue;
                        }

                        var isTag = entryStr[0] == TAG_PREFIX;
                        var entryId = ParseResourceLocation(isTag ? entryStr[1..] : entryStr);

                        if (isTag)
                        {
                            tag.tagReferences.Add(entryId);
                        }
                        else
                        {
                            tag.directEntries.Add(entryId);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to load tag file [{filePath}]: {e.Message}");
            }
        }

        private HashSet<ResourceLocation> GetResolvedEntries()
        {
            return resolvedEntries ??= ResolveEntries(new HashSet<ResourceLocation>());
        }

        private HashSet<ResourceLocation> ResolveEntries(HashSet<ResourceLocation> visiting)
        {
            if (resolvedEntries is not null)
            {
                return resolvedEntries;
            }

            if (!visiting.Add(TagId))
            {
                Debug.LogWarning($"Detected recursive tag reference in [{Type}] {TagId}");
                return new HashSet<ResourceLocation>();
            }

            var resolved = new HashSet<ResourceLocation>(directEntries);

            if (tagsByType.TryGetValue(Type, out var tagTable))
            {
                foreach (var tagRef in tagReferences)
                {
                    if (tagTable.TryGetValue(tagRef, out var refTag))
                    {
                        resolved.UnionWith(refTag.ResolveEntries(visiting));
                    }
                    else
                    {
                        Debug.LogWarning($"Missing tag reference in [{Type}] {TagId}: {tagRef}");
                    }
                }
            }

            visiting.Remove(TagId);
            resolvedEntries = resolved;
            return resolved;
        }

        private static ResourceLocation ParseResourceLocation(string value)
        {
            var trimmed = value.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                return ResourceLocation.INVALID;
            }

            var normalized = trimmed.Contains(':') ? trimmed : $"{DEFAULT_NAMESPACE}:{trimmed}";
            return ResourceLocation.FromString(normalized);
        }
    
        public override string ToString()
        {
            return $"{TAG_PREFIX}{TagId}";
        }
    }
}
