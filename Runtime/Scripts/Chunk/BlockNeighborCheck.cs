namespace CraftSharp
{
    public delegate bool BlockNeighborCheck(BlockState self, BlockState neighbor);

    public static class BlockNeighborChecks
    {
        public static readonly BlockNeighborCheck WATER_SURFACE = (_, neighbor)
            => !(neighbor.InWater || neighbor.MeshFaceOcclusionSolid);
        public static readonly BlockNeighborCheck LAVA_SURFACE  = (_, neighbor)
            => !(neighbor.InLava  || neighbor.MeshFaceOcclusionSolid);

        public static readonly BlockNeighborCheck NON_FULL_SOLID = (_, neighbor)
            => !neighbor.MeshFaceOcclusionSolid;
        
    }
}