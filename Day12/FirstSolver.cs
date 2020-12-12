using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day12
{
    // What is the Manhattan distance between that location and the ship's starting position?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var directions = new[] {'N', 'E', 'S', 'W'};
            var distances = new[] {0, 0, 0, 0};
            var currentDirIdx = 1;

            input.Select(l => (l[0], int.Parse(l[1..]))).ToList().ForEach(t =>
            {
                switch (t.Item1)
                {
                    case 'L':
                        currentDirIdx = ((4 - (t.Item2 / 90)) + currentDirIdx) % 4;
                        break;
                    case 'R':
                        currentDirIdx = (currentDirIdx + (t.Item2 / 90)) % 4;
                        break;
                    case 'F':
                        distances[currentDirIdx] += t.Item2;
                        break;
                    default:
                        distances[Array.FindIndex(directions, c => c.Equals(t.Item1))] += t.Item2;
                        break;
                }
            });

            return Math.Abs(distances[0] - distances[2]) + Math.Abs(distances[1] - distances[3]);
        }
    }
}