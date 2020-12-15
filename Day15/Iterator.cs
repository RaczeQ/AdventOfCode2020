using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public class Iterator
    {
        public static int Calculate(string input, int iterations)
        {
            var startingNumbers = input.Split(',').Select(int.Parse).ToList();
            var turns = startingNumbers.SkipLast(1)
                .Select((n, i) => (n, i))
                .ToDictionary(t => t.n, t => t.i);
            var lastNumber = startingNumbers[^1];
            for (int turn = turns.Count; turn < (iterations - 1); turn++)
            {
                var currentNumber = 0;
                if (turns.ContainsKey(lastNumber))
                {
                    currentNumber = turn - turns[lastNumber];
                }
                turns[lastNumber] = turn;
                lastNumber = currentNumber;
            }

            return lastNumber;
        }
    }
}
