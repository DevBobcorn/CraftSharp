using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftSharp
{
    public static class StringUtil
    {
        // See https://stackoverflow.com/a/63055998/21178367
        public static string ToUnderscoreCase(this string text)
        {
            if(text == null) {
                throw new ArgumentNullException(nameof(text));
            }
            if(text.Length < 2) {
                return text.ToLowerInvariant();
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for(int i = 1; i < text.Length; ++i) {
                char c = text[i];
                if(char.IsUpper(c)) {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                } else {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string ToRomanNumbers(int num)
        {
            if (num < 0) return "???";

            var sb = new StringBuilder();
            var romanNumbers = new Dictionary<string, int>
            {
                {"M", 1000}, {"CM", 900}, {"D", 500}, {"CD", 400},
                {"C", 100},  {"XC", 90},  {"L", 50},  {"XL", 40},
                {"X", 10},   {"IX", 9},   {"V", 5},   {"IV", 4},
                {"I", 1}
            };

            foreach (var pair in romanNumbers)
            {
                sb.Append(string.Join(string.Empty, Enumerable.Repeat(pair.Key, num / pair.Value)));
                num %= pair.Value;
            }

            return sb.ToString();
        }
    }
}