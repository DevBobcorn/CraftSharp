using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class MobAttributePalette : IdentifierPalette<MobAttribute>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly MobAttributePalette INSTANCE = new();
        protected override string Name => "MobAttribute Palette";
        protected override MobAttribute UnknownObject => MobAttribute.DUMMY_MOB_ATTRIBUTE;

        /// <summary>
        /// Load mob attribute data from external files.
        /// </summary>
        /// <param name="dataVersion">Mob attribute data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var mobAttributeListPath = PathHelper.GetExtraDataFile($"entities{SP}mob_attributes-{dataVersion}.json");

            if (!File.Exists(mobAttributeListPath))
            {
                Debug.LogWarning("MobAttribute data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var mobAttributes = Json.ParseJson(File.ReadAllText(mobAttributeListPath, Encoding.UTF8));

                foreach (var (key, mobAttributeDef) in mobAttributes.Properties)
                {
                    if (int.TryParse(mobAttributeDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var mobAttributeId = ResourceLocation.FromString(key);
                        
                        AddEntry(mobAttributeId, numId, new MobAttribute(mobAttributeId));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral mob attribute key [{key}]");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading mob attributes: {e.Message}");
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
