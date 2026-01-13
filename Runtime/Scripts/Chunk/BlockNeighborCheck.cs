namespace CraftSharp
{
    public delegate bool BlockNeighborCheck(BlockState self, BlockState neighbor, Direction direction);

    public static class BlockNeighborChecks
    {
        public static readonly BlockNeighborCheck WATER_SURFACE = (_, neighbor, direction)
            => !(neighbor.InWater || (neighbor.MeshFaceOcclusionSolid && direction != Direction.Up));
        public static readonly BlockNeighborCheck LAVA_SURFACE  = (_, neighbor, direction)
            => !(neighbor.IsLava  || (neighbor.MeshFaceOcclusionSolid && direction != Direction.Up));

        public static readonly BlockNeighborCheck NON_FULL_SOLID = (_, neighbor, _)
            => !neighbor.MeshFaceOcclusionSolid;
        
    }
}