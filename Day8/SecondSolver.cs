using System.Collections.Generic;
using System.Linq;
using ElvenTools;
using ElvenTools.Utils;

namespace Day8
{
    // Fix the program so that it terminates normally by changing exactly one jmp (to nop) or nop (to jmp). What is the value of the accumulator after the program terminates?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var instructions = input
                .Select(l =>
                {
                    var ops = l.Split(' ');
                    return (op: ops[0], val: int.Parse(ops[1]));
                }).ToList();

            var instructionsSets = new List<List<(string op, int val)>>();
            foreach (var ((op, val), index) in instructions.WithIndex())
            {
                var copy = false;
                var copiedInstructions = instructions.Select(inst => (inst.op, inst.val)).ToList();
                switch (op)
                {
                    case "nop":
                        copiedInstructions[index] = (op: "jmp", val);
                        copy = true;
                        break;
                    case "jmp":
                        copiedInstructions[index] = (op: "nop", val);
                        copy = true;
                        break;
                }

                if (copy)
                {
                    instructionsSets.Add(copiedInstructions);
                }
            }
            bool terminates;
            var currentSetIndex = 0;
            int accumulator;
            do
            {
                var currentInstructions = instructionsSets[currentSetIndex++];
                terminates = true;
                var visitedInstructions = new List<int>();
                var currentIndex = 0;
                accumulator = 0;
                while (currentIndex < currentInstructions.Count)
                {
                    var (op, val) = currentInstructions[currentIndex++];
                    switch (op)
                    {
                        case "acc":
                            accumulator += val;
                            break;
                        case "jmp":
                            currentIndex = --currentIndex + val;
                            break;
                    }
                    if (visitedInstructions.Contains(currentIndex))
                    {
                        terminates = false;
                        break;
                    }
                    visitedInstructions.Add(currentIndex);
                }
            } while (!terminates && currentSetIndex < instructionsSets.Count);

            return accumulator;
        }
    }
}