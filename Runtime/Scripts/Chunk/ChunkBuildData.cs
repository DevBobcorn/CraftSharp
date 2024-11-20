using Unity.Mathematics;

namespace CraftSharp
{
    public struct ChunkBuildData
    {
        public Block[,,] Blocks; // 18 * 18 * 18
        public byte[,,] Light; // 18 * 18 * 18
        public bool[,,] AO; // 18 * 18 * 18
        public float3[,,] Color; // 16 * 16 * 16

        public ChunkBuildData(Block[,,] blocks, byte[,,] light, bool[,,] ao, float3[,,] color)
        {
            Blocks = blocks;
            Light = light;
            AO = ao;
            Color = color;
        }
    }
}
