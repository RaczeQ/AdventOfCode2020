using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElvenTools;

namespace Day14
{
    // What is the sum of all values left in memory after it completes?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var registers = new Dictionary<string, string>();
            char[] currentMask = new char[0];
            foreach (var line in input)
            {
                switch (line[..3])
                {
                    case "mas":
                        currentMask = line[7..].ToCharArray();
                        break;
                    case "mem":
                        var regexGroups = new Regex(@"\[(.*)\] = (\d*)", RegexOptions.Compiled).Match(line).Groups;
                        var register = regexGroups[1].Value;
                        var binaryNumber = Convert.ToString(Convert.ToInt64(regexGroups[2].Value), 2).PadLeft(36, '0')
                            .ToCharArray();
                        registers[register] = String.Concat(currentMask.Zip(binaryNumber)
                            .Select(t => t.First.Equals('X') ? t.Second : t.First));
                        break;
                }
            }

            return registers.Values.Aggregate(Convert.ToInt64(0), ((l, s) => l + Convert.ToInt64(s, 2)));
        }
    }
}