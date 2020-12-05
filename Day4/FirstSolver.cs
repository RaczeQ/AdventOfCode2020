using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day4
{
    // Count the number of valid passports - those that have all required fields. Treat cid as optional. In your batch file, how many passports are valid?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            return string
                .Join("\n", input)
                .Split("\n\n")
                .Select(x => new Passport(x.Replace("\n", " ")))
                .Count(p => p.IsValid);
        }
    }
}