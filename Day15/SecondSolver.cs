using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ElvenTools;
using ElvenTools.Utils;

namespace Day15
{
    // Given your starting numbers, what will be the 30000000th number spoken?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return Iterator.Calculate(input[0], 30000000);
        }
    }
}