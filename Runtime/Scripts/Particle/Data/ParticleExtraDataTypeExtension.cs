using System;

namespace CraftSharp
{
    public static class ParticleExtraDataNetworkTypeExtension
    {
        public static Type GetDataType(this ParticleExtraDataType networkType)
        {
            return networkType switch
            {
                ParticleExtraDataType.None                => typeof (EmptyParticleExtraData),
                ParticleExtraDataType.Block               => typeof (BlockParticleExtraData),
                ParticleExtraDataType.Dust                => typeof (DustParticleExtraData),
                ParticleExtraDataType.DustColorTransition => typeof (DustColorTransitionParticleExtraData),
                ParticleExtraDataType.EntityEffect        => typeof (EntityEffectParticleExtraData),
                ParticleExtraDataType.SculkCharge         => typeof (SculkChargeParticleExtraData),
                ParticleExtraDataType.Item                => typeof (ItemParticleExtraData),
                ParticleExtraDataType.Vibration           => typeof (VibrationParticleExtraData),
                ParticleExtraDataType.Shriek              => typeof (ShriekParticleExtraData),
                
                _                                         => typeof (EmptyParticleExtraData),
            };
        }
    }
}
