namespace CraftSharp
{
    public record EntityEffectParticleExtraData : ParticleExtraData
    {
        /// <summary>
        /// The ARGB components of the color encoded as an int
        /// </summary>
        public int Color;

        public EntityEffectParticleExtraData(int color)
        {
            Color = color;
        }
    }
}