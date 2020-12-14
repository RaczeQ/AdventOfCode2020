using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ElvenTools;
using ElvenTools.Utils;

namespace Day14
{
    // What is the sum of all values left in memory after it completes?
    public class SecondSolver : ISolver
    {
        private List<long> ParseFloatingRegister(string register)
        {
            var result = new List<long>();
            var xIdx = register.ToList().FindIndex(c => c.Equals('X'));
            if (xIdx != -1)
            {
                for (int n = 0; n < 2; n++)
                {
                    result.AddRange(
                        ParseFloatingRegister(
                            String.Concat(register.Select((c, i) => i == xIdx ? n.ToString()[0] : c))));
                }
            }
            else
            {
                result.Add(Convert.ToInt64(register, 2));
            }

            return result;
        }

        public long Calculate(List<string> input)
        {
            var registers = new Dictionary<long, long>();
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
                        var register = Convert.ToString(Convert.ToInt64(regexGroups[1].Value), 2).PadLeft(36, '0')
                            .ToCharArray();
                        var maskedRegister = currentMask.Zip(register)
                            .Select(t => t.First.Equals('0') ? t.Second : t.First);
                        var number = Convert.ToInt64(regexGroups[2].Value);
                        ParseFloatingRegister(String.Concat(maskedRegister)).ForEach(r => registers[r] = number);
                        break;
                }
            }

            return registers.Values.Sum();
        }
    }
}