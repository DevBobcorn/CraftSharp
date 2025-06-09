using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class EnchantmentTypePalette : IdentifierPalette<EnchantmentType>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly EnchantmentTypePalette INSTANCE = new();
        protected override string Name => "EnchantmentType Palette";
        protected override EnchantmentType UnknownObject => EnchantmentType.DUMMY_ENCHANTMENT_TYPE;

        /// <summary>
        /// Load enchantment data from external files.
        /// </summary>
        /// <param name="dataVersion">Enchantment data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var enchantmentListPath = PathHelper.GetExtraDataFile($"items{SP}enchantment_types-{dataVersion}.json");

            if (!File.Exists(enchantmentListPath))
            {
                Debug.LogWarning("EnchantmentType data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var enchantmentTypes = Json.ParseJson(File.ReadAllText(enchantmentListPath, Encoding.UTF8));

                foreach (var (key, enchantmentDef) in enchantmentTypes.Properties)
                {
                    if (int.TryParse(enchantmentDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var enchantmentTypeId =  ResourceLocation.FromString(key);
                        var translationKey = enchantmentDef.Properties.TryGetValue("translation_key", out var val)
                            ? val.StringValue : enchantmentTypeId.GetTranslationKey("enchantment");

                        AddEntry(enchantmentTypeId, numId, new EnchantmentType(enchantmentTypeId, translationKey));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral enchantment type key [{key}]");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading enchantment type: {e.Message}");
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
