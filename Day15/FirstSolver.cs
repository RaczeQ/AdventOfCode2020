using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElvenTools;

namespace Day15
{
    // Given your starting numbers, what will be the 2020th number spoken?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return Iterator.Calculate(input[0], 2020);
        }
    }
}