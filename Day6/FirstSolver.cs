using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day6
{
    // For each group, count the number of questions to which anyone answered "yes". What is the sum of those counts?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return string.Join("\n", input)
                .Split("\n\n")
                .Select(x => x.Replace("\n", "").ToCharArray().Distinct().Count())
                .Sum();
        }
    }
}
