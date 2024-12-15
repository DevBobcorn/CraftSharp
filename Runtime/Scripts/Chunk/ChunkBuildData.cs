using Unity.Mathematics;

namespace CraftSharp
{
    public struct ChunkBuildData
    {
        public Block[,,] Blocks; // 18 * 18 * 18
        public byte[,,] Light; // 18 * 18 * 18
        public float3[,,] Color; // 16 * 16 * 16

        public ChunkBuildData(Block[,,] blocks, byte[,,] light, float3[,,] color)
        {
            Blocks = blocks;
            Light = light;
            Color = color;
        }
    }
}
