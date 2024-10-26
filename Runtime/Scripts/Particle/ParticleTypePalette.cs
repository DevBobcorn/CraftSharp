using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class ParticleTypePalette : IdentifierPalette<ParticleType>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly ParticleTypePalette INSTANCE = new();
        protected override string Name => "ParticleType Palette";
        protected override ParticleType UnknownObject => ParticleType.DUMMY_PARTICLE_TYPE;

        protected override void ClearEntries()
        {
            base.ClearEntries();
        }

        /// <summary>
        /// Load particle data from external files.
        /// </summary>
        /// <param name="dataVersion">Particle data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var particleTypePath = PathHelper.GetExtraDataFile($"particles{SP}particle_types-{dataVersion}.json");

            if (!File.Exists(particleTypePath))
            {
                Debug.LogWarning("Particle data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var particleTypes = Json.ParseJson(File.ReadAllText(particleTypePath, Encoding.UTF8));

                foreach (var particleType in particleTypes.Properties)
                {
                    var particleDef = particleType.Value;

                    if (int.TryParse(particleDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var particleTypeId = ResourceLocation.FromString(particleType.Key);

                        ParticleExtraDataType particleExtraData = ParticleExtraDataType.None;

                        if (particleDef.Properties.TryGetValue("extra_data", out Json.JSONData extraDataValue))
                        {
                            particleExtraData = extraDataValue.StringValue switch
                            {
                                "block"                 => ParticleExtraDataType.Block,
                                "dust"                  => ParticleExtraDataType.Dust,
                                "dust_color_transition" => ParticleExtraDataType.DustColorTransition,
                                "color"                 => ParticleExtraDataType.Color,
                                "sculk_charge"          => ParticleExtraDataType.SculkCharge,
                                "item"                  => ParticleExtraDataType.Item,
                                "vibration"             => ParticleExtraDataType.Vibration,
                                "shriek"                => ParticleExtraDataType.Shriek,

                                _                       => ParticleExtraDataType.None,
                            };
                        }

                        AddEntry(particleTypeId, numId, new ParticleType(particleTypeId, particleExtraData));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral particle type key [{particleType.Key}]");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading particle types: {e.Message}");
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
