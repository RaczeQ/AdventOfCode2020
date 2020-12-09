using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ElvenTools;
using ElvenTools.Utils;

namespace Day9
{
    // What is the first number that does not have this property?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            const int preambleSize = 25;
            var numbers = input.Select(long.Parse).ToList();
            return numbers
                .Skip(preambleSize)
                .Select((x, i) => (x, numbers.Skip(i).Take(preambleSize).ToList()))
                .First(t => t.Item2
                    .SelectMany((x, i) => t.Item2.Skip(i + 1), (x, y) => (x, y))
                    .All(p => p.x + p.y != t.x)).x;
        }
    }
}