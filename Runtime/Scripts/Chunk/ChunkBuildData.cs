using Unity.Mathematics;

namespace CraftSharp
{
    public struct ChunkBuildData
    {
        public BlockState[,,] BlockStates; // 18 * 18 * 18
        public int[,,] BlockStateIds; // 18 * 18 * 18
        public byte[,,] Light; // 18 * 18 * 18
        public float3[,,] Color; // 16 * 16 * 16

        public ChunkBuildData(BlockState[,,] blockStates, int[,,] blockStateIds, byte[,,] light, float3[,,] color)
        {
            BlockStates = blockStates;
            BlockStateIds = blockStateIds;
            Light = light;
            Color = color;
        }
    }
}
