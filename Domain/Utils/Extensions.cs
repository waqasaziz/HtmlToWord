using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain
{
    public static class Extensions
    {
        public static string ToHexString(this byte[] data)
        {
            var result = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                result.Append(data[i].ToString("X2"));

            return result.ToString();
        }

        public static byte[] ToByteArray(this string hex)
        {
            if (!Regex.IsMatch(hex, @"\A\b[0-9a-fA-F]+\b\Z"))
                return new byte[0];

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ToBase64String(this byte[] data) => Convert.ToBase64String(data);

        public static string TrimExtraSpaces(this string text) => Regex.Replace(text, @"\s+", " ");

        public static string ToUpperFirstLetter(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }


        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => source.Where(x => !predicate(x));
    }
}
