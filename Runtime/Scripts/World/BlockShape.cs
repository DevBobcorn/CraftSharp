using System;
using System.Globalization;
using UnityEngine;

namespace CraftSharp
{
    public record BlockShape
    {
        public static BlockShape EMPTY = new(new BlockShapeAABB[] { });

        public readonly BlockShapeAABB[] AABBs;

        public BlockShape(BlockShapeAABB[] aabbs)
        {
            AABBs = aabbs;
        }
    }

    public record BlockShapeAABB
    {
        public static BlockShapeAABB EMPTY = new(0, 0, 0, 0, 0, 0);

        public readonly float MinX;
        public readonly float MinY;
        public readonly float MinZ;
        public readonly float MaxX;
        public readonly float MaxY;
        public readonly float MaxZ;

        public float SizeX => MaxX - MinX;
        public float SizeY => MaxY - MinY;
        public float SizeZ => MaxZ - MinZ;

        public float CenterX => (MaxX + MinX) / 2F;
        public float CenterY => (MaxY + MinY) / 2F;
        public float CenterZ => (MaxZ + MinZ) / 2F;

        public static BlockShapeAABB FromString(string aabbString)
        {
            var strings = aabbString.Split(' ');
            var numbers = new float[6];

            try
            {
                for (int i = 0; i < 6; i++)
                {
                    // Always use '.' as decimal separator
                    numbers[i] = float.Parse(strings[i], CultureInfo.InvariantCulture.NumberFormat);
                }

                return new BlockShapeAABB(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5]);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Invalid AABB string: {aabbString}. {e}");

                return EMPTY;
            }
        }

        public BlockShapeAABB(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }
    }
}
