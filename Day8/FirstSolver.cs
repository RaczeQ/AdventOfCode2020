using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day8
{
    // Run your copy of the boot code. Immediately before any instruction is executed a second time, what value is in the accumulator?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var instructions = input
                .Select(l =>
                {
                    var ops = l.Split(' ');
                    return (op: ops[0], val: int.Parse(ops[1]));
                }).ToList();
            var visitedInstructions = new List<int>();
            var currentIndex = 0;
            var accumulator = 0;
            do
            {
                visitedInstructions.Add(currentIndex);
                var (op, val) = instructions[currentIndex++];
                switch (op)
                {
                    case "acc":
                        accumulator += val;
                        break;
                    case "jmp":
                        currentIndex = --currentIndex + val;
                        break;
                }
            } while (!visitedInstructions.Contains(currentIndex));

            return accumulator;
        }
    }
}