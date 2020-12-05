using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day5
{
    // As a sanity check, look through your list of boarding passes. What is the highest seat ID on a boarding pass?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return input
                .Select(BoardingPassDecoder.Transform)
                .OrderByDescending(l => l)
                .Select(BoardingPassDecoder.DecodePass)
                .Select(t => t.row * 8 + t.column)
                .First();
        }
    }
}