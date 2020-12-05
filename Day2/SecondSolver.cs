using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day2
{
    // How many passwords are valid according to the new interpretation of the policies?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var passwords = input.Select(l => new Password(l)).ToList();
            return passwords
                .Count(p => p.PasswordValue[p.FirstNumber - 1] == p.PolicyChar ^ p.PasswordValue[p.SecondNumber - 1] == p.PolicyChar);
        }
    }
}
