using UnityEngine;

namespace CraftSharp
{
    // Unity has a different coordinate system than Minecraft,
    // therefore we need to convert them in some occasions...
    // See https://minecraft.fandom.com/wiki/Coordinates
    public static class CoordConvert
    {
        public static Vector3 GetPosDelta(Vector3Int originOffsetDelta)
        {
            return -new Vector3(originOffsetDelta.x << 9, originOffsetDelta.y << 9, originOffsetDelta.z << 9);
        }

        public static Vector3 MC2Unity(Vector3Int originOffset, float x, float y, float z)
        {
            return new(z - (originOffset.x << 9), y - (originOffset.y << 9), x - (originOffset.z << 9));
        }

        public static Vector3 MC2UnityDelta(Location loc)
        {
            return new((float) loc.Z, (float) loc.Y, (float) loc.X);
        }

        public static Vector3 MC2Unity(Vector3Int originOffset, Location loc)
        {
            return new((float) (loc.Z - (originOffset.x << 9)), (float) (loc.Y - (originOffset.y << 9)), (float) (loc.X - (originOffset.z << 9)));
        }

        public static Vector3 MC2Unity(Vector3Int originOffset, int x, int y, int z)
        {
            return new(z - (originOffset.x << 9), y - (originOffset.y << 9), x - (originOffset.z << 9));
        }

        public static Vector3 MC2Unity(Vector3Int originOffset, Vector3 vec)
        {
            return new(vec.z - (originOffset.x << 9), vec.y - (originOffset.y << 9), vec.x - (originOffset.z << 9));
        }

        public static Location Unity2MC(Vector3Int originOffset, Vector3 vec)
        {
            return new(vec.z + (originOffset.z << 9), vec.y + (originOffset.y << 9), vec.x + (originOffset.x << 9));
        }
        
        public static BlockLoc Unity2MC(Vector3Int originOffset, Vector3Int vec)
        {
            return new(vec.z + (originOffset.z << 9), vec.y + (originOffset.y << 9), vec.x + (originOffset.x << 9));
        }
    }
}