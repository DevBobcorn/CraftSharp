namespace CraftSharp
{
    public record ShriekParticleExtraData : ParticleExtraData
    {
        public int Delay;

        public ShriekParticleExtraData(int delay)
        {
            Delay = delay;
        }
    }
}