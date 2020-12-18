using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ElvenTools;
using ElvenTools.Utils;

namespace Day18
{
    // What do you get if you add up the results of evaluating the homework problems using these new rules?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return input.Sum(line =>
            {
                var elements = line
                    .Replace("(", "( ")
                    .Replace(")", " )")
                    .Split(" ");
                var postfix = CustomPostfixCalculator.InfixToPostfix(elements, inverted: true);
                return CustomPostfixCalculator.Calculate(postfix);
            });
        }
    }
}