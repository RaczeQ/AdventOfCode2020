using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day2
{
    // How many passwords are valid according to their policies?
    public class FirstSolver : IBaseSolver
    {
        public int Calculate(List<string> input)
        {
            var passwords = input.Select(l => new Password(l)).ToList();
            return passwords
                .Select(p => (p.PasswordValue.Count(c => c == p.PolicyChar), p.FirstNumber, p.SecondNumber))
                .Count(t => t.Item1 >= t.FirstNumber && t.Item1 <= t.SecondNumber);
        }
    }
}
