using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp
{
    public class ParticleTypePalette : IdentifierPalette<BaseParticleType>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly ParticleTypePalette INSTANCE = new();
        protected override string Name => "ParticleType Palette";
        protected override BaseParticleType UnknownObject => ParticleType<EmptyParticleExtraData>.DUMMY_PARTICLE_TYPE;

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

                foreach (var (particleKey, particleDef) in particleTypes.Properties)
                {
                    if (int.TryParse(particleDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var particleTypeId = ResourceLocation.FromString(particleKey);

                        ParticleExtraDataType optionType = ParticleExtraDataType.None;

                        if (particleDef.Properties.TryGetValue("extra_data", out Json.JSONData extraDataValue))
                        {
                            optionType = extraDataValue.StringValue switch
                            {
                                "block"                 => ParticleExtraDataType.Block,
                                "dust"                  => ParticleExtraDataType.Dust,
                                "dust_color_transition" => ParticleExtraDataType.DustColorTransition,
                                "color"                 => ParticleExtraDataType.EntityEffect,
                                "sculk_charge"          => ParticleExtraDataType.SculkCharge,
                                "item"                  => ParticleExtraDataType.Item,
                                "vibration"             => ParticleExtraDataType.Vibration,
                                "shriek"                => ParticleExtraDataType.Shriek,

                                _                       => ParticleExtraDataType.None,
                            };
                        }

                        var optionClassType = optionType.GetDataType();
                        var particleClassType = typeof (ParticleType<>);
                        var newParticleClassType = particleClassType.MakeGenericType(optionClassType);

                        var newParticleType = (BaseParticleType) Activator.CreateInstance(
                            newParticleClassType, particleTypeId, optionType);

                        AddEntry(particleTypeId, numId, newParticleType);
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral particle type key [{particleKey}]");
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
