using Unity.Mathematics;

namespace CraftSharp
{
    public struct ChunkBuildData
    {
        public BlockState[,,] BlockStates; // 18 * 18 * 18
        public int[,,] BlockStateIds; // 18 * 18 * 18
        public byte[,,] Light; // 18 * 18 * 18
        public int[,,] Color; // 16 * 16 * 16
        public int[,,] Water; // 16 * 16 * 16

        public ChunkBuildData(BlockState[,,] blockStates, int[,,] blockStateIds, byte[,,] light, int[,,] color, int[,,] water)
        {
            BlockStates = blockStates;
            BlockStateIds = blockStateIds;
            Light = light;
            Color = color;
            Water = water;
        }
    }
}
