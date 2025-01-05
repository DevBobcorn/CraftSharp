namespace CraftSharp
{
    public record BlockParticleExtraData : ParticleExtraData
    {
        public int BlockStateId;

        public BlockParticleExtraData(int blockStateId)
        {
            BlockStateId = blockStateId;
        }
    }
}