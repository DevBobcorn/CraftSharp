namespace CraftSharp
{
    public record BaseParticleType
    {
        public readonly ResourceLocation TypeId;
        public readonly ParticleExtraDataType OptionType;

        public BaseParticleType(ResourceLocation id, ParticleExtraDataType optionType)
        {
            TypeId = id;
            OptionType = optionType;
        }

        public override string ToString()
        {
            return TypeId.ToString();
        }
    }

    /// <summary>
    /// Represents a Minecraft Particle Type
    /// </summary>
    public record ParticleType<T> : BaseParticleType where T : ParticleExtraData
    {
        public static readonly ParticleType<EmptyParticleExtraData> DUMMY_PARTICLE_TYPE =
                new(ResourceLocation.INVALID, ParticleExtraDataType.None);

        public ParticleType(ResourceLocation id, ParticleExtraDataType optionType) : base(id, optionType)
        {

        }
    }
}