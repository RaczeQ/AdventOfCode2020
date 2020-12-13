using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day13
{
    // What is the ID of the earliest bus you can take to the airport multiplied by the number of minutes you'll need to wait for that bus?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var timestamp = long.Parse(input[0]);
            return input[1]
                .Split(',')
                .Where(c => !c.Equals("x"))
                .Select(long.Parse)
                .Select(b => (b, Convert.ToInt64(b * Math.Ceiling((float) timestamp / (float) b) - timestamp)))
                .OrderBy(t => t.Item2)
                .Select(t => t.b * t.Item2)
                .First();
        }
    }
}