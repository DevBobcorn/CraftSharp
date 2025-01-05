namespace CraftSharp
{
    /// <summary>
    /// Data for 'minecraft:vibration' particle in 1.19.3+
    /// </summary>
    public record VibrationParticleExtraDataV2 : ParticleExtraData
    {
        // For 'block' position source
        public Location Position;

        // For 'entity' position source
        public int EntityId;
        public float EyeHeight;

        public int Ticks;
        public readonly bool UseBlockPosSource;

        public VibrationParticleExtraDataV2(Location position, int ticks)
        {
            Position = position;
            Ticks = ticks;

            UseBlockPosSource = true;
        }

        public VibrationParticleExtraDataV2(int entityId, float eyeHeight, int ticks)
        {
            EntityId = entityId;
            EyeHeight = eyeHeight;
            Ticks = ticks;

            UseBlockPosSource = true;
        }
    }
}