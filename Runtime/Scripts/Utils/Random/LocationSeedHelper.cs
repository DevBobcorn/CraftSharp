#nullable enable

namespace CraftSharp
{
    public static class LocationSeedHelper
    {
        public static long GetSeed(int i, int j, int k)
        {
            var l = i * 3129871 ^ j * 116129781L ^ k;
            l = l * l * 42317861L + l * 11L;
            return l >> 16;
        }
    }
}
