using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class EntityTypePalette : IdentifierPalette<EntityType>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly EntityTypePalette INSTANCE = new();
        protected override string Name => "EntityType Palette";
        protected override EntityType UnknownObject => EntityType.DUMMY_ENTITY_TYPE;

        /// <summary>
        /// Adds an entity type directly into the registry.
        /// <br/>
        /// This should be used for debugging purposes only, where actual entity data is not present.
        /// </summary>
        public void InjectEntityType(int numId, ResourceLocation identifier)
        {
            UnfreezeEntries();

            AddEntry(identifier, numId, new EntityType(identifier, 1F, 1F, true,
                new Dictionary<int, EntityMetaEntry>(), false, int.MaxValue));

            FreezeEntries();
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

            var entityTypePath = PathHelper.GetExtraDataFile($"entities{SP}entity_types-{dataVersion}.json");

            if (!File.Exists(entityTypePath))
            {
                Debug.LogWarning("Entity data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var entityTypes = Json.ParseJson(File.ReadAllText(entityTypePath, Encoding.UTF8));

                foreach (var (key, entityDef) in entityTypes.Properties)
                {
                    if (int.TryParse(entityDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var entityTypeId = ResourceLocation.FromString(key);

                        var w = float.Parse(entityDef.Properties["width"].StringValue,
                                CultureInfo.InvariantCulture.NumberFormat);
                        
                        var h = float.Parse(entityDef.Properties["height"].StringValue,
                                CultureInfo.InvariantCulture.NumberFormat);
                        
                        var sf = bool.Parse(entityDef.Properties["size_fixed"].StringValue);
                        
                        var ui = int.Parse(entityDef.Properties["update_interval"].StringValue,
                            CultureInfo.InvariantCulture.NumberFormat);

                        // Read entity meta entries
                        var metaEntries = entityDef.Properties["metadata"].Properties.
                                ToDictionary(x => int.Parse(x.Key),
                                        x => new EntityMetaEntry(x.Value.Properties["name"].StringValue,
                                            EntityMetadataTypeUtil.FromSerializedTypeName(
                                                x.Value.Properties["data_type"].StringValue)));

                        bool c = metaEntries.Values.Any(x => x.Name is "data_item" or "data_item_stack");

                        AddEntry(entityTypeId, numId, new EntityType(entityTypeId, w, h, sf, metaEntries, c, ui));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral entity type key [{key}]");
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
                flag.Finished = true;
            }
        }
    }
}
