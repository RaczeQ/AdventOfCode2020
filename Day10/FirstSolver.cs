using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;
using ElvenTools.Utils;

namespace Day10
{
    // What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var diffCounts = input
                .Select(long.Parse)
                .Concat(new long[] {0})
                .OrderBy(x => x)
                .Pairwise()
                .Aggregate(new Dictionary<long, long> {[1] = 0, [2] = 0, [3] = 0}, (longs, t) =>
                {
                    longs[t.Item2 - t.Item1]++;
                    return longs;
                });
            return diffCounts[1] * ++diffCounts[3];
        }
    }
}