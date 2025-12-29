#nullable enable
using System;
using System.Collections.Generic;

namespace CraftSharp
{
    public record BlockShape
    {
        public static readonly BlockShape EMPTY = new(new HashSet<ShapeAABB>(), null);

        public readonly HashSet<ShapeAABB> AABBs;
        public readonly HashSet<ShapeAABB>? ColliderAABBs;

        public BlockShape(HashSet<ShapeAABB> aabbs, HashSet<ShapeAABB>? colAabbs = null)
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
}
