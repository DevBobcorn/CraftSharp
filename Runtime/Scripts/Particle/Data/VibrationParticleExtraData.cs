namespace CraftSharp
{
    public record VibrationParticleExtraData : ParticleExtraData
    {
        // For 'block' position source
        public Location Position;

        // For 'entity' position source
        public int EntityId;
        public float EyeHeight;

        public int Ticks;
        public readonly bool UseBlockPosSource;

        public VibrationParticleExtraData(Location position, int ticks)
        {
            Position = position;
            Ticks = ticks;

            UseBlockPosSource = true;
        }

        public VibrationParticleExtraData(int entityId, float eyeHeight, int ticks)
        {
            EntityId = entityId;
            EyeHeight = eyeHeight;
            Ticks = ticks;

            UseBlockPosSource = true;
        }
    }
}