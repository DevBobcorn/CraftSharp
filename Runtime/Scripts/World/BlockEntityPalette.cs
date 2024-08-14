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
            //string listsPath  = PathHelper.GetExtraDataFile("block_entity_lists.json");
            string mappingPath  = PathHelper.GetExtraDataFile("block_entity_mapping.json");

            if (!File.Exists(blockEntityTypeListPath) || !File.Exists(mappingPath)) // || !File.Exists(listsPath))
            {
                Debug.LogWarning("BlockEntity data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            /* First read special block entity lists...
            var lists = new Dictionary<string, HashSet<ResourceLocation>>();
            lists.Add("contains_item", new());

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
            var containsItem = lists["contains_item"]; */

            try
            {
                var entityTypeList = Json.ParseJson(File.ReadAllText(blockEntityTypeListPath, Encoding.UTF8));

                foreach (var blockEntityType in entityTypeList.Properties)
                {
                    if (int.TryParse(blockEntityType.Key, out int numId))
                    {
                        var blockEntityTypeId = ResourceLocation.FromString(blockEntityType.Value.StringValue);

                        AddEntry(blockEntityTypeId, numId, new BlockEntityType(blockEntityTypeId));
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
            }

            // Read block entity mapping...
            var mappingData = Json.ParseJson(File.ReadAllText(mappingPath, Encoding.UTF8));
            foreach (var pair in mappingData.Properties)
            {
                var blockEntityTypeId = ResourceLocation.FromString(pair.Key);
                if (idToNumId.ContainsKey(blockEntityTypeId))
                {
                    foreach (var block in pair.Value.DataArray)
                    {
                        var blockId = ResourceLocation.FromString(block.StringValue);

                        if (BlockStatePalette.INSTANCE.Check(blockId))
                        {
                            blockEntityMapping.Add(blockId, GetById(blockEntityTypeId));
                        }
                        else
                        {
                            //Debug.LogWarning($"Block {blockId} which has {blockEntityTypeId} is not present!");
                        }
                    }
                }
                else
                {
                    //Debug.LogWarning($"Block entity type {blockEntityTypeId} is not loaded");
                }
            }

            flag.Finished = true;
        }
    }
}
