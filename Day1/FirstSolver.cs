using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day1
{
    // Find the two entries that sum to 2020; what do you get if you multiply them together?
    public class FirstSolver : IBaseSolver
    {
        public long Calculate(List<string> input)
        {
            var numbers = input.Select(int.Parse).ToList();
            return numbers
                .SelectMany((x, i) => numbers.Skip(i + 1), (x, y) => (x, y))
                .Where(t => t.x + t.y == 2020)
                .Select(t => t.x * t.y)
                .First();
        }
    }
}
