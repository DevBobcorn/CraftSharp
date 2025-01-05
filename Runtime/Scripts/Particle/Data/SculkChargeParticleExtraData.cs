namespace CraftSharp
{
    public record SculkChargeParticleExtraData : ParticleExtraData
    {
        /// <summary>
        /// The angle the particle displays at in radians
        /// </summary>
        public float Roll;

        public SculkChargeParticleExtraData(float roll)
        {
            Roll = roll;
        }
    }
}