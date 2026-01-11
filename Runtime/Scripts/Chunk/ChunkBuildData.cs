namespace CraftSharp
{
    public struct ChunkBuildData
    {
        public BlockLoc OriginBlockLoc;
        public BlockState[,,] BlockStates; // 18 * 18 * 18
        public int[,,] BlockStateIds; // 18 * 18 * 18
        public byte[,,] Light; // 18 * 18 * 18
        public int[,,] Color; // 16 * 16 * 16
        public int[,,] Water; // 16 * 16 * 16
    }
}
