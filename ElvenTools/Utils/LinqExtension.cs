using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElvenTools.Utils
{
    public static class LinqExtension
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self.Select((item, index) => (item, index));
        
        public static IEnumerable<(int x, int y)> TobogganRange(int xSlope, int ySlope, int count)
        {
            for (int n = 1; n * ySlope < count; n += 1)
            {
                yield return (xSlope * n, ySlope * n);
            }
        }

        public static IEnumerable<Tuple<T, T>> Pairwise<T>(this IEnumerable<T> source)
        {
            var previous = default(T);

            using (var it = source.GetEnumerator())
            {
                if (it.MoveNext())
                    previous = it.Current;

                while (it.MoveNext())
                    yield return Tuple.Create(previous, previous = it.Current);
            }
        }
    }
}
