using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static string ToBase64String(this byte[] data) => Convert.ToBase64String(data);

        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => source.Where(x => !predicate(x));
    }
}
