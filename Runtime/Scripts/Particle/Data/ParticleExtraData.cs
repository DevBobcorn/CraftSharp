namespace CraftSharp
{
    public abstract record ParticleExtraData
    {
        public record EmptyParticleExtraData : ParticleExtraData { }

        public static readonly EmptyParticleExtraData Empty = new();
    }
}