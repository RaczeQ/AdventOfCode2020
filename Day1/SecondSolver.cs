using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;

namespace Day1
{
    // In your expense report, what is the product of the three entries that sum to 2020?
    public class SecondSolver : IBaseSolver
    {
        public int Calculate(List<string> input)
        {
            var numbers = input.Select(int.Parse).ToList();
            var result = numbers
                .SelectMany(
                    (x, i) => numbers.Skip(i + 1).SelectMany(
                        (y, j) => numbers.Skip(i + j + 1),
                        (y, z) => (y, z)),
                    (x, t) => (x, t.y, t.z)
                )
                .Where(t => t.x + t.y + t.z == 2020)
                .Select(t => t.x * t.y * t.z)
                .First();
            return result;
        }
    }
}
