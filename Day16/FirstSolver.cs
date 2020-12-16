using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElvenTools;

namespace Day16
{
    // Consider the validity of the nearby tickets you scanned. What is your ticket scanning error rate?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var ruleRegex = new Regex(@"(.*): (\d*)-(\d*) or (\d*)-(\d*)", RegexOptions.Compiled);
            var yourTicketIdx = input.FindIndex(l => l.StartsWith("your ticket"));
            var nearbyTicketIdx = input.FindIndex(l => l.StartsWith("nearby tickets"));
            
            var rules = input
                .Take(yourTicketIdx - 1)
                .Select(l => ruleRegex.Match(l).Groups)
                .SelectMany(g => new [] {(int.Parse(g[2].Value), int.Parse(g[3].Value)),
                    (int.Parse(g[4].Value), int.Parse(g[5].Value))
                }).ToList();
            return input
                .Skip(nearbyTicketIdx + 1)
                .SelectMany(l => l.Split(','))
                .Select(int.Parse)
                .Where(n => rules.All(t => n < t.Item1 || n > t.Item2))
                .Sum();
        }
    }
}