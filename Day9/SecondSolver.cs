using System.Collections.Generic;
using System.Linq;
using ElvenTools;
using ElvenTools.Utils;

namespace Day9
{
    // 
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            const int preambleSize = 25;
            var numbers = input.Select(long.Parse).ToList();
            var invalidNumber = numbers.Skip(preambleSize)
                .Select((x, i) => (x, numbers.Skip(i).Take(preambleSize).ToList()))
                .First(t => t.Item2
                    .SelectMany((x, i) => t.Item2.Skip(i + 1), (x, y) => (x, y))
                    .All(p => p.x + p.y != t.x)).x;

            var currentIdx = 0;
            var setSize = 1;
            long currentSum = 0;

            while (true)
            {
                currentSum += numbers[currentIdx + setSize++ - 1];
                if (currentSum == invalidNumber)
                {
                    var range = numbers.Skip(currentIdx).Take(--setSize).ToList();
                    return range.Min() + range.Max();
                }

                if (currentSum <= invalidNumber) continue;
                
                currentIdx++;
                setSize = 1;
                currentSum = 0;
            }
        }
    }
}