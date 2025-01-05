namespace CraftSharp
{
    /// <summary>
    /// Data for 'minecraft:vibration' particle in 1.17 - 1.19.2
    /// </summary>
    public record VibrationParticleExtraDataV1 : ParticleExtraData
    {
        public Location Origin;
        public Location Destination;
        public int Ticks;

        public VibrationParticleExtraDataV1(Location origin, Location destination, int ticks)
        {
            Origin = origin;
            Destination = destination;
            Ticks = ticks;
        }
    }
}