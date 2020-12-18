using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ElvenTools;
using ElvenTools.Utils;

namespace Day17
{
    // Starting with your given initial configuration, simulate six cycles in a 4-dimensional space. How many cubes are left in the active state after the sixth cycle?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var coordinates = input
                .SelectMany((l, y) => l
                    .Select((c, x) => (string.Join(',', new[] { x, y, 0, 0 }), b: c.Equals('#'))))
                .ToDictionary(t => t.Item1, t => t.b);
            
            return ConwayEvaluator.Evaluate(coordinates);
        }
    }
}