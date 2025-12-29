#nullable enable
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace CraftSharp
{
    public static class RandomSupport
    {
        private const long GOLDEN_RATIO_64 = -7046029254386353131L;
        private const long SILVER_RATIO_64 = 7640891576956012809L;
        private static readonly MD5 MD5_128 = MD5.Create();
        private static long seedUniquifier = 8682522807148012L;
        private static readonly object seedUniquifierLock = new object();

        private static long MixStafford13(long l)
        {
            l = (l ^ (long)((ulong)l >> 30)) * -4658895280553007687L;
            l = (l ^ (long)((ulong)l >> 27)) * -7723592293110705685L;
            return l ^ (long)((ulong)l >> 31);
        }

        private static Seed128bit UpgradeSeedTo128bitUnmixed(long l)
        {
            var m = l ^ SILVER_RATIO_64;
            var n = m + GOLDEN_RATIO_64;
            return new Seed128bit(m, n);
        }

        public static Seed128bit UpgradeSeedTo128bit(long l)
        {
            return UpgradeSeedTo128bitUnmixed(l).Mixed();
        }

        public static Seed128bit SeedFromHashOf(string str)
        {
            var bs = MD5_128.ComputeHash(Encoding.UTF8.GetBytes(str));
            var l = BitConverter.ToInt64(bs, 0);
            var m = BitConverter.ToInt64(bs, 8);
            return new Seed128bit(l, m);
        }

        public static long GenerateUniqueSeed()
        {
            long current;
            long next;
            do
            {
                current = seedUniquifier;
                next = current * 1181783497276652981L;
            } while (Interlocked.CompareExchange(ref seedUniquifier, next, current) != current);
            
            // Use high-resolution timer similar to System.nanoTime()
            var nanoTime = (DateTime.UtcNow.Ticks - DateTime.MinValue.Ticks) * 100; // Approximate nanoseconds
            return next ^ nanoTime;
        }

        public class Seed128bit
        {
            private long SeedLo { get; }
            private long SeedHi { get; }

            public Seed128bit(long seedLo, long seedHi)
            {
                SeedLo = seedLo;
                SeedHi = seedHi;
            }

            private Seed128bit Xor(long l, long m)
            {
                return new Seed128bit(SeedLo ^ l, SeedHi ^ m);
            }

            public Seed128bit Xor(Seed128bit seed128bit)
            {
                return Xor(seed128bit.SeedLo, seed128bit.SeedHi);
            }

            public Seed128bit Mixed()
            {
                return new Seed128bit(RandomSupport.MixStafford13(this.SeedLo), RandomSupport.MixStafford13(this.SeedHi));
            }
        }
    }
}

