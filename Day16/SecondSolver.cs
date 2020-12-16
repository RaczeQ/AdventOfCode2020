using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ElvenTools;
using ElvenTools.Utils;

namespace Day16
{
    // Once you work out which field is which, look for the six fields on your ticket that start with the word departure. What do you get if you multiply those six values together?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var ruleRegex = new Regex(@"(.*): (\d*)-(\d*) or (\d*)-(\d*)", RegexOptions.Compiled);
            var yourTicketIdx = input.FindIndex(l => l.StartsWith("your ticket"));
            var nearbyTicketIdx = input.FindIndex(l => l.StartsWith("nearby tickets"));

            var rules = input
                .Take(yourTicketIdx - 1)
                .Select(l => ruleRegex.Match(l).Groups)
                .Select(g => (
                    (int.Parse(g[2].Value), int.Parse(g[3].Value)),
                    (int.Parse(g[4].Value), int.Parse(g[5].Value)),
                    g[1].Value
                ))
                .Select(t => (
                    Enumerable.Range(t.Item1.Item1, t.Item1.Item2 - t.Item1.Item1 + 1),
                    Enumerable.Range(t.Item2.Item1, t.Item2.Item2 - t.Item2.Item1 + 1),
                    t.Item3
                ))
                .ToList();
            
            var yourTicket = input[yourTicketIdx + 1].Split(',').Select(long.Parse).ToList();
            var validTickets = input
                .Skip(nearbyTicketIdx + 1)
                .Select(l => l.Split(',').Select(long.Parse))
                .Where(t => t.All(n => rules.Any(r => r.Item1.Contains((int) n) || r.Item2.Contains((int) n))))
                .Concat(new[] {yourTicket})
                .ToList();

            var rulesIdxs = rules
                .Select(r =>
                    (r.Item3, Enumerable.Range(0, rules.Count)
                        .Where(idx => validTickets
                            .Select(t => t.ElementAt(idx))
                            .All(n => r.Item1.Contains((int) n) || r.Item2.Contains((int) n)))
                        .ToList()))
                .OrderBy(l => l.Item2.Count)
                .ToList();

            return rulesIdxs
                .Select(tuple =>
                {
                    if (tuple.Item2.Count > 1)
                    {
                        throw new ArgumentException("Invalid iteration - non ambiguous rule id!");
                    }

                    var idx = tuple.Item2[0];
                    rulesIdxs.ForEach(t => t.Item2.Remove(idx));
                    return (tuple.Item1, idx);
                })
                .Where(t => t.Item1.StartsWith("departure"))
                .Select(t => yourTicket.ElementAt(t.idx)).Aggregate((a, n) => a * n);
        }
    }
}