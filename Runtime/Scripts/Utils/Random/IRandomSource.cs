#nullable enable
using System;

namespace CraftSharp
{
    public interface IRandomSource
    {
        public const double GAUSSIAN_SPREAD_FACTOR = 2.297;

        static IRandomSource Create()
        {
            return Create(RandomSupport.GenerateUniqueSeed());
        }

        static IRandomSource Create(long l)
        {
            return new LegacyRandomSource(l);
        }

        void SetSeed(long l);

        int NextInt();

        int NextInt(int i);

        int NextIntBetweenInclusive(int i, int j)
        {
            return this.NextInt(j - i + 1) + i;
        }

        long NextLong();

        bool NextBoolean();

        float NextFloat();

        double NextDouble();

        double NextGaussian();

        double Triangle(double d, double e)
        {
            return d + e * (NextDouble() - NextDouble());
        }

        float Triangle(float f, float g)
        {
            return f + g * (NextFloat() - NextFloat());
        }

        void ConsumeCount(int i)
        {
            for (var j = 0; j < i; ++j)
            {
                NextInt();
            }
        }

        int NextInt(int i, int j)
        {
            if (i >= j)
            {
                throw new ArgumentException("bound - origin is non positive");
            }
            else
            {
                return i + NextInt(j - i);
            }
        }
    }
}

