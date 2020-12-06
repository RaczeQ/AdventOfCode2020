using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using ElvenTools;

namespace Day6
{
    // For each group, count the number of questions to which everyone answered "yes". What is the sum of those counts?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return string.Join("\n", input)
                .Split("\n\n")
                .Select(x => x.Split("\n").Select(l => l.ToCharArray().Distinct()))
                .Select(g => g.Aggregate((prev, next) => prev.Intersect(next).ToList()).Count())
                .Sum();
        }
    }
}