using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ElvenTools;
using ElvenTools.Utils;

namespace Day3
{
    // What do you get if you multiply together the number of trees encountered on each of the listed slopes?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var plane = new TobogganPlane(input);
            var slopes = new List<(int x, int y)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2),
            };
            
            return slopes
                .Select(s => LinqExtension
                    .TobogganRange(s.x, s.y, input.Count)
                    .Count((t) => plane.IsTree(t.x, t.y)))
                .Select(Convert.ToInt64)
                .Aggregate((a, x) => a * x);
        }
    }
}
