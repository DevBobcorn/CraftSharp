using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

namespace CraftSharp
{
    public class ItemPalette : IdentifierPalette<Item>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;
        public static readonly ItemPalette INSTANCE = new();
        protected override string Name => "Item Palette";
        protected override Item UnknownObject => Item.UNKNOWN;

        private readonly Dictionary<ResourceLocation, Func<ItemStack, float3[]>> itemColorRules = new();


        public bool IsTintable(ResourceLocation identifier)
        {
            return itemColorRules.ContainsKey(identifier);
        }

        public Func<ItemStack, float3[]> GetTintRule(ResourceLocation identifier)
        {
            if (itemColorRules.ContainsKey(identifier))
                return itemColorRules[identifier];
            return null;
        }

        protected override void ClearEntries()
        {
            base.ClearEntries();
            itemColorRules.Clear();
        }

        /// <summary>
        /// Load item data from external files.
        /// </summary>
        /// <param name="dataVersion">Item data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            string itemsPath = PathHelper.GetExtraDataFile($"items{SP}items-{dataVersion}.json");
            string colorsPath = PathHelper.GetExtraDataFile("item_colors.json");

            if (!File.Exists(itemsPath) || !File.Exists(colorsPath))
            {
                Debug.LogWarning("Item data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var items = Json.ParseJson(File.ReadAllText(itemsPath, Encoding.UTF8));

                foreach (var item in items.Properties)
                {
                    if (int.TryParse(item.Value.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var itemId = ResourceLocation.FromString(item.Key);

                        ItemRarity rarity = item.Value.Properties["rarity"].StringValue switch
                        {
                            "common"   => ItemRarity.Common,
                            "uncommon" => ItemRarity.Uncommon,
                            "rare"     => ItemRarity.Rare,
                            "epic"     => ItemRarity.Epic,

                            _          => ItemRarity.Common
                        };

                        int stackLimit = int.Parse(item.Value.Properties["stack_limit"].StringValue);
                        bool edible = bool.Parse(item.Value.Properties["edible"].StringValue);

                        ResourceLocation? itemBlockId = null;

                        if (item.Value.Properties.TryGetValue("block", out Json.JSONData blockId))
                        {
                            itemBlockId = ResourceLocation.FromString(blockId.StringValue);
                        }

                        Item newItem = new(itemId, stackLimit, rarity, edible, itemBlockId);

                        AddEntry(itemId, numId, newItem);
                    }
                }

                // Hardcoded placeholder types for internal and network use
                AddDirectionalEntry(Item.UNKNOWN.ItemId, -2, Item.UNKNOWN);
                AddDirectionalEntry(Item.NULL.ItemId,    -1, Item.NULL);

                // Load item color rules...
                Json.JSONData colorRules = Json.ParseJson(File.ReadAllText(colorsPath, Encoding.UTF8));

                if (colorRules.Properties.ContainsKey("fixed"))
                {
                    foreach (var fixedRule in colorRules.Properties["fixed"].Properties)
                    {
                        var itemId = ResourceLocation.FromString(fixedRule.Key);

                        if (idToNumId.TryGetValue(itemId, out int numId))
                        {
                            var fixedColor = VectorUtil.Json2Float3(fixedRule.Value) / 255F;
                            float3[] ruleFunc(ItemStack itemStack) => new float3[] { fixedColor };

                            if (!itemColorRules.TryAdd(itemId, ruleFunc))
                            {
                                Debug.LogWarning($"Failed to apply fixed color rules to {itemId} ({numId})!");
                            }
                        }
                        else
                        {
                            //Debug.LogWarning($"Applying fixed color rules to undefined item {itemId}!");
                        }
                    }
                }

                if (colorRules.Properties.ContainsKey("fixed_multicolor"))
                {
                    foreach (var fixedRule in colorRules.Properties["fixed_multicolor"].Properties)
                    {
                        var itemId = ResourceLocation.FromString(fixedRule.Key);

                        if (idToNumId.TryGetValue(itemId, out int numId))
                        {
                            var colorList = fixedRule.Value.DataArray.ToArray();
                            var fixedColors = new float3[colorList.Length];

                            for (int c = 0;c < colorList.Length;c++)
                                fixedColors[c] = VectorUtil.Json2Float3(colorList[c]) / 255F;

                            float3[] ruleFunc(ItemStack itemStack) => fixedColors;

                            if (!itemColorRules.TryAdd(itemId, ruleFunc))
                            {
                                Debug.LogWarning($"Failed to apply fixed multi-color rules to {itemId} ({numId})!");
                            }
                        }
                        else
                        {
                            //Debug.LogWarning($"Applying fixed multi-color rules to undefined item {itemId}!");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading items: {e.Message}");
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
