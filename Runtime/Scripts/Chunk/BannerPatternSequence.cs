using System;
using System.Linq;

namespace CraftSharp
{
    public record BannerPatternRecord(ResourceLocation Type, CommonColors Color)
    {
        public ResourceLocation Type { get; } = Type;
        public CommonColors Color { get; } = Color;

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Color.GetHashCode();
        }
    }
    
    public class BannerPatternSequence
    {
        public readonly BannerPatternRecord[] records;

        public BannerPatternSequence(BannerPatternRecord[] records)
        {
            this.records = records;
        }

        public override int GetHashCode()
        {
            return records.Aggregate(0, (current, rec) => HashCode.Combine(rec, current));
        }

        public override string ToString()
        {
            return string.Join(", ", records.Select(x => $"[{x.Type}, {x.Color}]")) + $"Hash: {GetHashCode()}";
        }
    }
}