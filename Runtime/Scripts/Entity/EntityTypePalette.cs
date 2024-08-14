using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class EntityTypePalette : IdentifierPalette<EntityType>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly EntityTypePalette INSTANCE = new();
        public override string Name => "EntityType Palette";
        protected override EntityType UnknownObject => EntityType.DUMMY_ENTITY_TYPE;

        /// <summary>
        /// Adds an entity type directly into the registry.
        /// <br/>
        /// This should be used for debugging purposes only, where actual entity data is not present.
        /// </summary>
        public void InjectEntityType(int numId, ResourceLocation identifier)
        {
            UnfreezeEntries();

            AddEntry(identifier, numId, new(identifier));

            FreezeEntries();
        }

        protected override void ClearEntries()
        {
            base.ClearEntries();
        }

        /// <summary>
        /// Load entity data from external files.
        /// </summary>
        /// <param name="dataVersion">Entity data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var entityTypeListPath = PathHelper.GetExtraDataFile($"entities{SP}entity_types-{dataVersion}.json");
            string listsPath  = PathHelper.GetExtraDataFile("entity_lists.json");

            if (!File.Exists(entityTypeListPath) || !File.Exists(listsPath))
            {
                Debug.LogWarning("Entity data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            // First read special entity lists...
            var lists = new Dictionary<string, HashSet<ResourceLocation>>
            {
                { "contains_item", new() }
            };

            Json.JSONData spLists = Json.ParseJson(File.ReadAllText(listsPath, Encoding.UTF8));
            foreach (var pair in lists)
            {
                if (spLists.Properties.ContainsKey(pair.Key))
                {
                    foreach (var block in spLists.Properties[pair.Key].DataArray)
                        pair.Value.Add(ResourceLocation.FromString(block.StringValue));
                }
            }

            // References for later use
            var containsItem = lists["contains_item"];

            try
            {
                var entityTypeList = Json.ParseJson(File.ReadAllText(entityTypeListPath, Encoding.UTF8));

                foreach (var entityType in entityTypeList.Properties)
                {
                    if (int.TryParse(entityType.Key, out int numId))
                    {
                        var entityTypeId = ResourceLocation.FromString(entityType.Value.StringValue);

                        AddEntry(entityTypeId, numId, new EntityType(
                                entityTypeId, containsItem.Contains(entityTypeId)));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral entity type key [{entityType.Key}]");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading entity types: {e.Message}");
                flag.Failed = true;
            }
            finally
            {
                FreezeEntries();
            }

            flag.Finished = true;
        }
    }
}
