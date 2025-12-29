#nullable enable

namespace CraftSharp
{
    public class LegacyRandomSource : IBitRandomSource
    {
        private const int MODULUS_BITS = 48;
        private const long MODULUS_MASK = 281474976710655L;
        private const long MULTIPLIER = 25214903917L;
        private const long INCREMENT = 11L;
        private long seed;
        private readonly object seedLock = new();
        private readonly MarsagliaPolarGaussian gaussianSource;

        public LegacyRandomSource(long l)
        {
            gaussianSource = new MarsagliaPolarGaussian(this);
            InitializeSeed(l);
        }

        private void InitializeSeed(long l)
        {
            lock (seedLock)
            {
                seed = (l ^ MULTIPLIER) & MODULUS_MASK;
                gaussianSource.Reset();
            }
        }

        public virtual void SetSeed(long l)
        {
            InitializeSeed(l);
        }

        public int Next(int i)
        {
            lock (seedLock)
            {
                var l = seed;
                var m = (l * MULTIPLIER + INCREMENT) & MODULUS_MASK;
                seed = m;
                return (int)(m >> (MODULUS_BITS - i));
            }
        }

        public double NextGaussian()
        {
            return gaussianSource.NextGaussian();
        }
    }
}

