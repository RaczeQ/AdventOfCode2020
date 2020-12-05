using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day3
{
    // Starting at the top-left corner of your map and following a slope of right 3 and down 1, how many trees would you encounter?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var plane = new TobogganPlane(input);
            return RangeExtension
                .TobogganRange(3, 1, input.Count)
                .Count((t) => plane.IsTree(t.x, t.y));
        }
    }
}