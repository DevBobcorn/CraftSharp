using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class PotionPalette : IdentifierPalette<Potion>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly PotionPalette INSTANCE = new();
        protected override string Name => "Potion Palette";
        protected override Potion UnknownObject => Potion.DUMMY_POTION;

        /// <summary>
        /// Load potion data from external files.
        /// </summary>
        /// <param name="dataVersion">Potion data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var potionListPath = PathHelper.GetExtraDataFile($"items{SP}potions-{dataVersion}.json");

            if (!File.Exists(potionListPath))
            {
                Debug.LogWarning("Potion data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var potions = Json.ParseJson(File.ReadAllText(potionListPath, Encoding.UTF8));

                foreach (var (key, potionDef) in potions.Properties)
                {
                    if (int.TryParse(potionDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var potionId = ResourceLocation.FromString(key);
                        var effects = potionDef.Properties["effects"]
                            .DataArray.Select(MobEffectInstance.FromJson).ToArray();

                        AddEntry(potionId, numId, new Potion(potionId, effects));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral potion key [{key}]");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading potions: {e.Message}");
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
