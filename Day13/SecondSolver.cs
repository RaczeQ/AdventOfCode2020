using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ElvenTools;
using ElvenTools.Utils;

namespace Day13
{
    // What is the earliest timestamp such that all of the listed bus IDs depart at offsets matching their positions in the list?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var x = input[1]
                .Split(',')
                .Select((item, index) => (item, index))
                .Where(t => !t.item.Equals("x"))
                .Select(t => (long.Parse(t.item), t.index))
                .OrderByDescending(t => t.Item1)
                .ToList();

            var timestamp = x.First().Item1 - x.First().index;
            var period = x.First().Item1;
            for (var busIndex = 1; busIndex <= x.Count; busIndex++)
            {
                while (x.Take(busIndex).Any(t => (timestamp + t.index) % t.Item1 != 0))
                {
                    timestamp += period;
                }

                period = x.Take(busIndex).Select(t => t.Item1).Aggregate(MathExtension.LCM);
            }

            return timestamp;
        }
    }
}