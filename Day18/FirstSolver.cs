using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElvenTools;

namespace Day18
{
    // Evaluate the expression on each line of the homework; what is the sum of the resulting values?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return input.Sum(line =>
            {
                var elements = line
                    .Replace("(", "( ")
                    .Replace(")", " )")
                    .Split(" ");
                var postfix = CustomPostfixCalculator.InfixToPostfix(elements, inverted: false);
                return CustomPostfixCalculator.Calculate(postfix);
            });
        }
    }
}