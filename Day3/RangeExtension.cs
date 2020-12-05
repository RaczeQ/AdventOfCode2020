using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public static class RangeExtension
    {
        public static IEnumerable<(int x, int y)> TobogganRange(int xSlope, int ySlope, int count)
        {
            for (int n = 1; n * ySlope < count; n += 1)
            {
                yield return (xSlope * n, ySlope * n);
            }
        }
    }
}
