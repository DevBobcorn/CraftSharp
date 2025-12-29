#nullable enable
using System;

namespace CraftSharp
{
    public class WorldgenRandom : LegacyRandomSource
    {
        private readonly IRandomSource randomSource;

        public WorldgenRandom(IRandomSource randomSource) : base(0L)
        {
            this.randomSource = randomSource;
        }

        public override void SetSeed(long l)
        {
            randomSource.SetSeed(l);
        }
    }
}

