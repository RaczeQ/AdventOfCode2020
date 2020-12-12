using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day12
{
    // What is the Manhattan distance between that location and the ship's starting position?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var directions = new[] { 'N', 'E', 'S', 'W' };
            var shipDistances = new[] { 0, 0, 0, 0 };
            var waypointDistances = new[] { 1, 10, 0, 0 };

            input.Select(l => (l[0], int.Parse(l[1..]))).ToList().ForEach(t =>
            {
                var sliceIdx = t.Item2 / 90;
                switch (t.Item1)
                {
                    case 'L':
                        waypointDistances = waypointDistances[sliceIdx..].Concat(waypointDistances[0..sliceIdx]).ToArray();
                        break;
                    case 'R':
                        waypointDistances = waypointDistances[^sliceIdx..^0].Concat(waypointDistances[..^sliceIdx]).ToArray();
                        break;
                    case 'F':
                        shipDistances = shipDistances.Zip(waypointDistances, (x, y) => x + y * t.Item2).ToArray();
                        break;
                    default:
                        waypointDistances[Array.FindIndex(directions, c => c.Equals(t.Item1))] += t.Item2;
                        break;
                }
            });
            return Math.Abs(shipDistances[0] - shipDistances[2]) + Math.Abs(shipDistances[1] - shipDistances[3]);
        }
    }
}
