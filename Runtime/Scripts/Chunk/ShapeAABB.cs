#nullable enable
using System;
using System.Globalization;
using UnityEngine;

namespace CraftSharp
{
    public readonly struct ShapeAABB : IEquatable<ShapeAABB>
    {
        private static readonly ShapeAABB EMPTY = new(0, 0, 0, 0, 0, 0);

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

        public static ShapeAABB FromString(string aabbString)
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

                return new ShapeAABB(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5]);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Invalid AABB string: {aabbString}. {e}");

                return EMPTY;
            }
        }

        public ShapeAABB WithOffset(float x, float y, float z)
        {
            return new ShapeAABB(MinX + x, MinY + y, MinZ + z, MaxX + x, MaxY + y, MaxZ + z);
        }

        public ShapeAABB(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }

        public bool Equals(ShapeAABB other)
        {
            return MinX.Equals(other.MinX) && MinY.Equals(other.MinY) && MinZ.Equals(other.MinZ) && MaxX.Equals(other.MaxX) && MaxY.Equals(other.MaxY) && MaxZ.Equals(other.MaxZ);
        }

        public override bool Equals(object? obj)
        {
            return obj is ShapeAABB other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MinX, MinY, MinZ, MaxX, MaxY, MaxZ);
        }
    }
}