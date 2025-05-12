namespace CraftSharp
{
    public delegate bool BlockNeighborCheck(BlockState self, BlockState neighbor);

    public class BlockNeighborChecks
    {
        public static readonly BlockNeighborCheck WATER_SURFACE = new((self, neighbor)
                => { return !(neighbor.InWater || neighbor.MeshFaceOcclusionSolid); });
        public static readonly BlockNeighborCheck LAVA_SURFACE  = new((self, neighbor)
                => { return !(neighbor.InLava  || neighbor.MeshFaceOcclusionSolid); });

        public static readonly BlockNeighborCheck NON_FULL_SOLID = new((self, neighbor)
                => { return !neighbor.MeshFaceOcclusionSolid; });
        
    }
}