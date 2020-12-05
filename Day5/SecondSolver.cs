using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ElvenTools;
using ElvenTools.Utils;

namespace Day5
{
    // What is the ID of your seat?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return input
                .Select(BoardingPassDecoder.Transform)
                .OrderBy(l => l)
                .Select(BoardingPassDecoder.DecodePass)
                .Select(t => t.row * 8 + t.column)
                .Pairwise()
                .First(t => t.Item1 == t.Item2 - 2).Item1 + 1;
        }
    }
}
