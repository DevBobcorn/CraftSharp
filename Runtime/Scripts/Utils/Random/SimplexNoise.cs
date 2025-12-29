#nullable enable
using System;

namespace CraftSharp
{
    public class SimplexNoise
    {
        private static readonly int[][] GRADIENT = new int[][]
        {
            new int[] { 1, 1, 0 }, new int[] { -1, 1, 0 }, new int[] { 1, -1, 0 }, new int[] { -1, -1, 0 },
            new int[] { 1, 0, 1 }, new int[] { -1, 0, 1 }, new int[] { 1, 0, -1 }, new int[] { -1, 0, -1 },
            new int[] { 0, 1, 1 }, new int[] { 0, -1, 1 }, new int[] { 0, 1, -1 }, new int[] { 0, -1, -1 },
            new int[] { 1, 1, 0 }, new int[] { 0, -1, 1 }, new int[] { -1, 1, 0 }, new int[] { 0, -1, -1 }
        };

        private static readonly double SQRT_3 = Math.Sqrt(3.0D);
        private static readonly double F2 = 0.5F * (SQRT_3 - 1.0D);
        private static readonly double G2 = (3.0D - SQRT_3) / 6.0D;

        private readonly int[] p = new int[512];
        public readonly double xo;
        public readonly double yo;
        public readonly double zo;

        public SimplexNoise(IRandomSource randomSource)
        {
            xo = randomSource.NextDouble() * 256.0D;
            yo = randomSource.NextDouble() * 256.0D;
            zo = randomSource.NextDouble() * 256.0D;

            for (var i = 0; i < 256; ++i)
            {
                p[i] = i;
            }

            for (var i = 0; i < 256; ++i)
            {
                var j = randomSource.NextInt(256 - i);
                (p[i], p[j + i]) = (p[j + i], p[i]);
            }
        }

        private int P(int i)
        {
            return p[i & 255];
        }

        private static double Dot(int[] is_, double d, double e, double f)
        {
            return is_[0] * d + is_[1] * e + is_[2] * f;
        }

        private static double GetCornerNoise3D(int i, double d, double e, double f, double g)
        {
            var h = g - d * d - e * e - f * f;
            double j;
            if (h < 0.0D)
            {
                j = 0.0D;
            }
            else
            {
                h *= h;
                j = h * h * Dot(GRADIENT[i], d, e, f);
            }

            return j;
        }

        public double GetValue(double d, double e)
        {
            var f = (d + e) * F2;
            var i = (int) Math.Floor(d + f);
            var j = (int) Math.Floor(e + f);
            var g = (i + j) * G2;
            var h = i - g;
            var k = j - g;
            var l = d - h;
            var m = e - k;
            int n;
            int o;
            if (l > m)
            {
                n = 1;
                o = 0;
            }
            else
            {
                n = 0;
                o = 1;
            }

            var p = l - n + G2;
            var q = m - o + G2;
            var r = l - 1.0D + 2.0D * G2;
            var s = m - 1.0D + 2.0D * G2;
            var t = i & 255;
            var u = j & 255;
            var v = P(t + P(u)) % 12;
            var w = P(t + n + P(u + o)) % 12;
            var x = P(t + 1 + P(u + 1)) % 12;
            var y = GetCornerNoise3D(v, l, m, 0.0D, 0.5D);
            var z = GetCornerNoise3D(w, p, q, 0.0D, 0.5D);
            var aa = GetCornerNoise3D(x, r, s, 0.0D, 0.5D);
            return 70.0D * (y + z + aa);
        }

        public double GetValue(double d, double e, double f)
        {
            const double g = 0.3333333333333333;
            var h = (d + e + f) * g;
            var i = (int)Math.Floor(d + h);
            var j = (int)Math.Floor(e + h);
            var k = (int)Math.Floor(f + h);
            const double l = 0.16666666666666666;
            var m = (i + j + k) * l;
            var n = i - m;
            var o = j - m;
            var p = k - m;
            var q = d - n;
            var r = e - o;
            var s = f - p;
            int t;
            int u;
            int v;
            int w;
            int x;
            int y;
            if (q >= r)
            {
                if (r >= s)
                {
                    t = 1;
                    u = 0;
                    v = 0;
                    w = 1;
                    x = 1;
                    y = 0;
                }
                else if (q >= s)
                {
                    t = 1;
                    u = 0;
                    v = 0;
                    w = 1;
                    x = 0;
                    y = 1;
                }
                else
                {
                    t = 0;
                    u = 0;
                    v = 1;
                    w = 1;
                    x = 0;
                    y = 1;
                }
            }
            else if (r < s)
            {
                t = 0;
                u = 0;
                v = 1;
                w = 0;
                x = 1;
                y = 1;
            }
            else if (q < s)
            {
                t = 0;
                u = 1;
                v = 0;
                w = 0;
                x = 1;
                y = 1;
            }
            else
            {
                t = 0;
                u = 1;
                v = 0;
                w = 1;
                x = 1;
                y = 0;
            }

            var z = q - t + l;
            var aa = r - u + l;
            var ab = s - v + l;
            var ac = q - w + g;
            var ad = r - x + g;
            var ae = s - y + g;
            var af = q - 1.0D + 0.5D;
            var ag = r - 1.0D + 0.5D;
            var ah = s - 1.0D + 0.5D;
            var ai = i & 255;
            var aj = j & 255;
            var ak = k & 255;
            var al = this.P(ai + this.P(aj + this.P(ak))) % 12;
            var am = this.P(ai + t + this.P(aj + u + this.P(ak + v))) % 12;
            var an = this.P(ai + w + this.P(aj + x + this.P(ak + y))) % 12;
            var ao = this.P(ai + 1 + this.P(aj + 1 + this.P(ak + 1))) % 12;
            var ap = GetCornerNoise3D(al, q, r, s, 0.6);
            var aq = GetCornerNoise3D(am, z, aa, ab, 0.6);
            var ar = GetCornerNoise3D(an, ac, ad, ae, 0.6);
            var as_ = GetCornerNoise3D(ao, af, ag, ah, 0.6);
            return 32.0D * (ap + aq + ar + as_);
        }
    }
}

