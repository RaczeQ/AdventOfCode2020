using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day2
{
    public class Password
    {
        public readonly string PasswordValue;
        public readonly char PolicyChar;
        public readonly int FirstNumber;
        public readonly int SecondNumber;

        public Password(string text)
        {
            Regex rx = new Regex(@"(\d*)-(\d*)\s(.):\s(.*)", RegexOptions.Compiled);
            Match match = rx.Match(text);
            FirstNumber = int.Parse(match.Groups[1].Value);
            SecondNumber = int.Parse(match.Groups[2].Value);
            PolicyChar = match.Groups[3].Value[0];
            PasswordValue = match.Groups[4].Value;
        }
    }
}