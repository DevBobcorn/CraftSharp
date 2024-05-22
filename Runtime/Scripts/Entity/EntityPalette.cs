using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class EntityPalette
    {
        private static readonly char SP = Path.DirectorySeparatorChar;
        public static readonly EntityPalette INSTANCE = new();

        private readonly Dictionary<int, EntityType> entityTypeTable = new();
        private readonly Dictionary<ResourceLocation, int> dictId = new();

        public static readonly EntityType UNKNOWN_ENTITY_TYPE = new(UNKNOWN_ENTITY_NUM_ID, new("unknown_entity"));
        public const int UNKNOWN_ENTITY_NUM_ID = -1;

        /// <summary>
        /// Get entity type from numeral id.
        /// </summary>
        /// <param name="id">Entity type ID</param>
        /// <returns>EntityType corresponding to the specified ID</returns>
        public EntityType FromNumId(int id)
        {
            //1.14+ entities have the same set of IDs regardless of living status
            if (entityTypeTable.ContainsKey(id))
                return entityTypeTable[id];

            return UNKNOWN_ENTITY_TYPE;
        }

        /// <summary>
        /// Get numeral id from entity type identifier.
        /// </summary>
        public int ToNumId(ResourceLocation identifier)
        {
            if (dictId.ContainsKey(identifier))
                return dictId[identifier];
            
            Debug.LogWarning($"Unknown Entity Type {identifier}");
            return UNKNOWN_ENTITY_NUM_ID;
        }

        /// <summary>
        /// Get entity type from entity type identifier.
        /// </summary>
        public EntityType FromId(ResourceLocation identifier)
        {
            return FromNumId(ToNumId(identifier));
        }

        /// <summary>
        /// Check if an entity type is present in the registry.
        /// <br/>
        /// This should be used for debugging purposes only.
        /// </summary>
        public bool CheckEntityType(ResourceLocation identifier)
        {
            return dictId.ContainsKey(identifier);
        }

        /// <summary>
        /// Adds an entity type directly into the registry.
        /// <br/>
        /// This should be used for debugging purposes only, where actual entity data is not present.
        /// </summary>
        public void InjectEntityType(int numId, ResourceLocation identifier)
        {
            if (dictId.ContainsKey(identifier))
            {
                Debug.LogWarning($"Entity type {identifier} is already registered with num id {dictId[identifier]}");
            }
            else if (entityTypeTable.ContainsKey(numId)) // Num id already occupied, assign another
            {
                while (entityTypeTable.ContainsKey(numId))
                {
                    numId += 1;
                }

                entityTypeTable[numId] = new(numId, identifier);
                dictId[identifier] = numId;
                //Debug.Log($"Entity type {identifier} is registered with num id {numId}");
            }
            else // Add this entry
            {
                entityTypeTable[numId] = new(numId, identifier);
                dictId[identifier] = numId;
                //Debug.Log($"Entity type {identifier} is registered with num id {numId}");
            }
        }

        /// <summary>
        /// Load entity data from external files.
        /// </summary>
        /// <param name="dataVersion">Entity data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            entityTypeTable.Clear();
            dictId.Clear();

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

                        entityTypeTable.TryAdd(numId, new EntityType(numId,
                                entityTypeId, containsItem.Contains(entityTypeId)));

                        dictId.TryAdd(entityTypeId, numId);
                    }
                    else
                        Debug.LogWarning($"Invalid numeral entity type key [{entityType.Key}]");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading entity types: {e.Message}");
                flag.Failed = true;
            }

            flag.Finished = true;
        }
    }
}
