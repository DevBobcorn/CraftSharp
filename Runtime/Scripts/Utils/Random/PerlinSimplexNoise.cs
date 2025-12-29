#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftSharp
{
    public class PerlinSimplexNoise
    {
        private readonly SimplexNoise[] noiseLevels;
        private readonly double highestFreqValueFactor;
        private readonly double highestFreqInputFactor;

        public PerlinSimplexNoise(IRandomSource randomSource, List<int> list)
        {
            var intSortedSet = new SortedSet<int>(list);
            
            if (intSortedSet.Count == 0)
            {
                throw new ArgumentException("Need some octaves!");
            }
            else
            {
                var i = -intSortedSet.Min;
                var j = intSortedSet.Max;
                var k = i + j + 1;
                if (k < 1)
                {
                    throw new ArgumentException("Total number of octaves needs to be >= 1");
                }
                else
                {
                    var simplexNoise = new SimplexNoise(randomSource);
                    var l = j;
                    
                    noiseLevels = new SimplexNoise[k];
                    
                    if (j >= 0 && j < k && intSortedSet.Contains(0))
                    {
                        noiseLevels[j] = simplexNoise;
                    }

                    for (var m = j + 1; m < k; ++m)
                    {
                        if (m >= 0 && intSortedSet.Contains(l - m))
                        {
                            noiseLevels[m] = new SimplexNoise(randomSource);
                        }
                        else
                        {
                            randomSource.ConsumeCount(262);
                        }
                    }

                    if (j > 0)
                    {
                        var n = (long)(simplexNoise.GetValue(simplexNoise.xo, simplexNoise.yo, simplexNoise.zo) * long.MaxValue);
                        IRandomSource randomSource2 = new WorldgenRandom(new LegacyRandomSource(n));

                        for (var o = l - 1; o >= 0; --o)
                        {
                            if (o < k && intSortedSet.Contains(l - o))
                            {
                                noiseLevels[o] = new SimplexNoise(randomSource2);
                            }
                            else
                            {
                                randomSource2.ConsumeCount(262);
                            }
                        }
                    }

                    highestFreqInputFactor = Math.Pow(2.0D, j);
                    highestFreqValueFactor = 1.0D / (Math.Pow(2.0D, k) - 1.0D);
                }
            }
        }

        public double GetValue(double d, double e, bool bl)
        {
            var f = 0.0D;
            var g = highestFreqInputFactor;
            var h = highestFreqValueFactor;

            foreach (var simplexNoise in noiseLevels)
            {
                // TODO: Check
                if (simplexNoise != null)
                {
                    f += simplexNoise.GetValue(d * g + (bl ? simplexNoise.xo : 0.0D), e * g + (bl ? simplexNoise.yo : 0.0D)) * h;
                }

                g /= 2.0D;
                h *= 2.0D;
            }

            return f;
        }
    }
}

