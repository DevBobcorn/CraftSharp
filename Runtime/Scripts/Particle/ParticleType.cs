using System.Collections.Generic;
using System.Linq;

namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft Particle Type
    /// </summary>
    public record ParticleType
    {
        public static readonly ParticleType DUMMY_PARTICLE_TYPE = new(ResourceLocation.INVALID, ParticleExtraDataType.None);

        public readonly ResourceLocation TypeId;
        public readonly ParticleExtraDataType ExtraDataType;

        public ParticleType(ResourceLocation id, ParticleExtraDataType extraDataType)
        {
            TypeId = id;
            ExtraDataType = extraDataType;
        }

        public override string ToString()
        {
            return TypeId.ToString();
        }
    }
}