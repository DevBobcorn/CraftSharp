#nullable enable
using System;

namespace CraftSharp
{
    public class MarsagliaPolarGaussian
    {
        public readonly IRandomSource RandomSource;
        private double nextNextGaussian;
        private bool haveNextNextGaussian;

        public MarsagliaPolarGaussian(IRandomSource randomSource)
        {
            RandomSource = randomSource;
        }

        public void Reset()
        {
            haveNextNextGaussian = false;
        }

        public double NextGaussian()
        {
            if (haveNextNextGaussian)
            {
                haveNextNextGaussian = false;
                return nextNextGaussian;
            }
            else
            {
                double d;
                double e;
                double f;
                do
                {
                    d = 2.0F * RandomSource.NextDouble() - 1.0F;
                    e = 2.0F * RandomSource.NextDouble() - 1.0F;
                    f = d * d + e * e;
                } while (f >= 1.0F || f == 0.0F);

                var g = Math.Sqrt(-2.0F * Math.Log(f) / f);
                nextNextGaussian = e * g;
                haveNextNextGaussian = true;
                return d * g;
            }
        }
    }
}

