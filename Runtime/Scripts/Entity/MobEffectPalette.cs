using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class MobEffectPalette : IdentifierPalette<MobEffect>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly MobEffectPalette INSTANCE = new();
        protected override string Name => "MobEffect Palette";
        protected override MobEffect UnknownObject => MobEffect.DUMMY_MOB_EFFECT;

        /// <summary>
        /// Load mob effect data from external files.
        /// </summary>
        /// <param name="dataVersion">Mob effect data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var mobEffectListPath = PathHelper.GetExtraDataFile($"entities{SP}mob_effects-{dataVersion}.json");

            if (!File.Exists(mobEffectListPath))
            {
                Debug.LogWarning("MobEffect data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var mobEffects = Json.ParseJson(File.ReadAllText(mobEffectListPath, Encoding.UTF8));

                foreach (var (key, mobEffectDef) in mobEffects.Properties)
                {
                    if (int.TryParse(mobEffectDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var mobEffectId = ResourceLocation.FromString(key);
                        var color = mobEffectDef.Properties.TryGetValue("color", out var val) ? int.Parse(val.StringValue) : 0;

                        var category = MobEffectCategory.Neutral;
                        if (mobEffectDef.Properties.TryGetValue("category", out val))
                        {
                            category = val.StringValue.ToLower() switch
                            {
                                "beneficial" => MobEffectCategory.Beneficial,
                                "harmful" => MobEffectCategory.Harmful,
                                "neutral" => MobEffectCategory.Neutral,

                                _      => MobEffectCategory.Neutral
                            };
                        }
                        

                        AddEntry(mobEffectId, numId, new MobEffect(mobEffectId, category, color));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral block entity type key [{key}]");
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
