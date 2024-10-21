using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class BlockEntityTypePalette : IdentifierPalette<BlockEntityType>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly BlockEntityTypePalette INSTANCE = new();
        public override string Name => "BlockEntityType Palette";
        protected override BlockEntityType UnknownObject => BlockEntityType.DUMMY_BLOCK_ENTITY_TYPE;

        private readonly Dictionary<ResourceLocation, BlockEntityType> blockEntityMapping = new();

        public bool GetBlockEntityForBlock(ResourceLocation blockId, out BlockEntityType blockEntityType)
        {
            var found = blockEntityMapping.TryGetValue(blockId, out BlockEntityType result);
            blockEntityType = result;
            return found;
        }

        protected override void ClearEntries()
        {
            base.ClearEntries();
            blockEntityMapping.Clear();
        }

        /// <summary>
        /// Load block entity data from external files.
        /// </summary>
        /// <param name="dataVersion">Block data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var blockEntityTypeListPath = PathHelper.GetExtraDataFile($"blocks{SP}block_entity_types-{dataVersion}.json");

            if (!File.Exists(blockEntityTypeListPath))
            {
                Debug.LogWarning("BlockEntity data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var blockEntityTypes = Json.ParseJson(File.ReadAllText(blockEntityTypeListPath, Encoding.UTF8));

                foreach (var blockEntityType in blockEntityTypes.Properties)
                {
                    var blockEntityDef = blockEntityType.Value;

                    if (int.TryParse(blockEntityDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var blockEntityTypeId = ResourceLocation.FromString(blockEntityType.Key);
                        var associatedBlockIds = new HashSet<ResourceLocation>();

                        foreach (var block in blockEntityDef.Properties["blocks"].DataArray)
                        {
                            var blockId = ResourceLocation.FromString(block.StringValue);

                            if (BlockStatePalette.INSTANCE.Check(blockId))
                            {
                                blockEntityMapping.Add(blockId, GetById(blockEntityTypeId));
                                associatedBlockIds.Add(blockId);
                            }
                            else
                            {
                                Debug.LogWarning($"Block {blockId} which has block entity {blockEntityTypeId} is not present!");
                            }
                        }

                        AddEntry(blockEntityTypeId, numId, new BlockEntityType(blockEntityTypeId, associatedBlockIds));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral block entity type key [{blockEntityType.Key}]");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading block entity types: {e.Message}");
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
