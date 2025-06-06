#nullable enable

namespace CraftSharp
{
    public static class BlockLocExtension
    {
        /// <summary>
        /// Get the X index of the corresponding chunk in the world
        /// </summary>
        public static int GetChunkX(this BlockLoc loc)
        {
            return loc.X >> 4;
        }

        /// <summary>
        /// Get the Y index of the corresponding chunk in the world
        /// </summary>
        public static int GetChunkYIndex(this BlockLoc loc, int minY)
        {
            return (loc.Y - minY) >> 4;
        }

        /// <summary>
        /// Get the Z index of the corresponding chunk in the world
        /// </summary>
        public static int GetChunkZ(this BlockLoc loc)
        {
            return loc.Z >> 4;
        }

        /// <summary>
        /// Get the X index of the corresponding block in the corresponding chunk of the world
        /// </summary>
        public static int GetChunkBlockX(this BlockLoc loc)
        {
            return loc.X & 0xF;
        }

        /// <summary>
        /// Get the Y index of the corresponding block in the corresponding chunk of the world
        /// </summary>
        public static int GetChunkBlockY(this BlockLoc loc)
        {
            return loc.Y & 0xF;
        }

        /// <summary>
        /// Get the Z index of the corresponding block in the corresponding chunk of the world
        /// </summary>
        public static int GetChunkBlockZ(this BlockLoc loc)
        {
            return loc.Z & 0xF;
        }
    }
}