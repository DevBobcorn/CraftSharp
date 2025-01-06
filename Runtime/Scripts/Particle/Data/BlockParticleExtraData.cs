namespace CraftSharp
{
    public record BlockParticleExtraData : ParticleExtraData
    {
        public int BlockStateId;
        public BlockState BlockState;

        public BlockParticleExtraData(int blockStateId, BlockState blockState)
        {
            BlockStateId = blockStateId;
            BlockState = blockState;
        }
    }
}