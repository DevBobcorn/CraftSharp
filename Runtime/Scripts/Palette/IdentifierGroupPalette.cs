using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CraftSharp
{
    /// <summary>
    /// Palette for object groups containing objects which share a same
    /// identifier but have different numeral(protocol) ids 
    /// </summary>
    public abstract class IdentifierGroupPalette<T> : ProtocolIdPalette<T>
    {
        protected record IdentifierGroup
        {
            public readonly int DefaultNumId;
            public readonly HashSet<int> NumIds;

            public IdentifierGroup(int defaultNumId, HashSet<int> numIds)
            {
                DefaultNumId = defaultNumId;
                NumIds = numIds;
            }
        }

        public static readonly ResourceLocation UNKNOWN_ID = ResourceLocation.INVALID;

        protected readonly Dictionary<ResourceLocation, IdentifierGroup> groupIdToGroup = new();
        protected readonly Dictionary<int, ResourceLocation> numIdToGroupId = new();

        protected override void ClearEntries()
        {
            base.ClearEntries();
            groupIdToGroup.Clear();
            numIdToGroupId.Clear();
        }

        /// <summary>
        /// Get all identifiers in the palette
        /// </summary>
        /// <returns></returns>
        public ResourceLocation[] GetAllGroupIds()
        {
            return groupIdToGroup.Keys.ToArray();
        }

        /// <summary>
        /// Check if given id is present
        /// </summary>
        public bool Check(ResourceLocation id)
        {
            return groupIdToGroup.ContainsKey(id);
        }

        /// <summary>
        /// Get all objects in the group by id, or empty array if group is not found
        /// </summary>
        public T[] GetAll(ResourceLocation id, Func<T, bool> filter = null)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                if (filter is null)
                    return group.NumIds.Select(x => numIdToObject[x]).ToArray();
                else
                    return group.NumIds.Select(x => numIdToObject[x]).Where(x => filter(x)).ToArray();
            }

            return new T[] { };
        }

        /// <summary>
        /// Get numIds of all objects in the group by id, or empty array if group is not found
        /// </summary>
        public int[] GetAllNumIds(ResourceLocation id, Func<T, bool> filter = null)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                if (filter is null)
                    return group.NumIds.ToArray();
                else
                    return group.NumIds.Select(x => numIdToObject[x]).Where(x => filter(x)).Select(x => objectToNumId[x]).ToArray();
            }

            return new int[] { };
        }

        /// <summary>
        /// Try to get all objects in the group by id, or empty array if group is not found,
        /// returns true if groupId is present AND results are not empty.
        /// </summary>
        public bool TryGetAll(ResourceLocation id, out T[] results, Func<T, bool> filter = null)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                if (filter is null)
                    results = group.NumIds.Select(x => numIdToObject[x]).ToArray();
                else
                    results = group.NumIds.Select(x => numIdToObject[x]).Where(x => filter(x)).ToArray();
                
                return results.Length > 0;
            }

            results = new T[] { };
            return false;
        }

        /// <summary>
        /// Try to get numIds of all objects in the group by id, or empty array if group is not found,
        /// returns true if groupId is present AND results are not empty.
        /// </summary>
        public bool TryGetAllNumIds(ResourceLocation id, out int[] results, Func<T, bool> filter = null)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                if (filter is null)
                    results = group.NumIds.ToArray();
                else
                    results = group.NumIds.Select(x => numIdToObject[x]).Where(x => filter(x)).Select(x => objectToNumId[x]).ToArray();
                
                return results.Length > 0;
            }

            results = new int[] { };
            return false;
        }

        /// <summary>
        /// Get default object in the group by id, or unknown object if group is not found
        /// </summary>
        public T GetDefault(ResourceLocation id)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                return numIdToObject[group.DefaultNumId];
            }

            return UnknownObject;
        }

        /// <summary>
        /// Get numId of default object in the group by id, or unknown object if group is not found
        /// </summary>
        public int GetDefaultNumId(ResourceLocation id)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                return group.DefaultNumId;
            }

            return UNKNOWN_NUM_ID;
        }

        /// <summary>
        /// Try to get default object in the group by id, or unknown object if group is not found
        /// </summary>
        public bool TryGetDefault(ResourceLocation id, out T result)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                result = numIdToObject[group.DefaultNumId];
                return true;
            }

            result = UnknownObject;
            return false;
        }

        /// <summary>
        /// Try to get all objects in the group by id, or unknown object if group is not found
        /// </summary>
        public bool TryGetDefaultNumId(ResourceLocation id, out int result)
        {
            if (groupIdToGroup.TryGetValue(id, out IdentifierGroup group))
            {
                result = group.DefaultNumId;
                return true;
            }

            result = UNKNOWN_NUM_ID;
            return false;
        }

        /// <summary>
        /// Get groupId by object, or invalid id if not found
        /// </summary>
        public ResourceLocation GetGroupIdByObject(T obj)
        {
            if (obj is not null && objectToNumId.TryGetValue(obj, out int numId))
            {
                return numIdToGroupId[numId];
            }

            return UNKNOWN_ID;
        }

        /// <summary>
        /// Get groupId by numId, or invalid id if not found
        /// </summary>
        public ResourceLocation GetGroupIdByNumId(int numId)
        {
            if (numIdToGroupId.TryGetValue(numId, out ResourceLocation id))
            {
                return id;
            }
            
            return UNKNOWN_ID;
        }

        /// <summary>
        /// Add an entry to the palette
        /// </summary>
        protected virtual void AddEntry(ResourceLocation groupId, int defaultNumId, Dictionary<int, T> objs)
        {
            if (!groupIdToGroup.ContainsKey(groupId))
            {
                var group = new IdentifierGroup(defaultNumId, objs.Keys.ToHashSet());

                groupIdToGroup.Add(groupId, group);

                foreach (var (numId, obj) in objs)
                {
                    if (!numIdToObject.ContainsKey(numId))
                    {
                        if (!objectToNumId.ContainsKey(obj))
                        {
                            numIdToObject.Add(numId, obj);
                            objectToNumId.Add(obj, numId);
                            numIdToGroupId.Add(numId, groupId);
                        }
                        else
                        {
                            Debug.LogWarning($"Object already registered in {Name}: {obj}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Numeral id already registered in {Name}: {numId}");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Identifier already registered in {Name}: {groupId}");
            }
        }
    }
}