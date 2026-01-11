#nullable enable
using System;

namespace CraftSharp
{
    public abstract class BitRandomSource : IRandomSource
    {
        public const float FLOAT_MULTIPLIER = 5.9604645E-8F;
        public const double DOUBLE_MULTIPLIER = 1.110223E-16F;
        
        public abstract void SetSeed(long l);
        
        public abstract double NextGaussian();

        protected abstract int Next(int i);

        public int NextInt()
        {
            return Next(32);
        }

        public int NextInt(int i)
        {
            if (i <= 0)
            {
                throw new ArgumentException("Bound must be positive");
            }
            else if ((i & (i - 1)) == 0)
            {
                return (int)(i * (long) Next(31) >> 31);
            }
            else
            {
                int j;
                int k;
                do
                {
                    j = Next(31);
                    k = j % i;
                } while (j - k + (i - 1) < 0);

                return k;
            }
        }

        public long NextLong()
        {
            var i = Next(32);
            var j = Next(32);
            var l = (long) i << 32;
            return l + j;
        }

        public bool NextBoolean()
        {
            return Next(1) != 0;
        }

        public float NextFloat()
        {
            return Next(24) * FLOAT_MULTIPLIER;
        }

        public double NextDouble()
        {
            var i = Next(26);
            var j = Next(27);
            var l = ((long) i << 27) + j;
            return l * DOUBLE_MULTIPLIER;
        }
    }
}

