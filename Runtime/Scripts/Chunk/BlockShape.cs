#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace CraftSharp
{
    public record BlockShape
    {
        public static readonly BlockShape EMPTY = new(new HashSet<BlockShapeAABB>(), null);

        public readonly HashSet<BlockShapeAABB> AABBs;
        public readonly HashSet<BlockShapeAABB>? ColliderAABBs;

        public BlockShape(HashSet<BlockShapeAABB> aabbs, HashSet<BlockShapeAABB>? colAabbs = null)
        {
            AABBs = aabbs;
            ColliderAABBs = colAabbs;
        }

        public virtual bool Equals(BlockShape? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return AABBs.Equals(other.AABBs) && Equals(ColliderAABBs, other.ColliderAABBs);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AABBs, ColliderAABBs);
        }
    }

    public readonly struct BlockShapeAABB : IEquatable<BlockShapeAABB>
    {
        private static readonly BlockShapeAABB EMPTY = new(0, 0, 0, 0, 0, 0);

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

        public BlockShapeAABB WithOffset(float x, float y, float z)
        {
            return new BlockShapeAABB(MinX + x, MinY + y, MinZ + z, MaxX + x, MaxY + y, MaxZ + z);
        }

        private BlockShapeAABB(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }

        public bool Equals(BlockShapeAABB other)
        {
            return MinX.Equals(other.MinX) && MinY.Equals(other.MinY) && MinZ.Equals(other.MinZ) && MaxX.Equals(other.MaxX) && MaxY.Equals(other.MaxY) && MaxZ.Equals(other.MaxZ);
        }

        public override bool Equals(object? obj)
        {
            return obj is BlockShapeAABB other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MinX, MinY, MinZ, MaxX, MaxY, MaxZ);
        }
    }
}
