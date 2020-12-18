using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools.Utils;

namespace Day17
{
    public class ConwayEvaluator
    {
        public static long Evaluate(Dictionary<string, bool> coordinates)
        {
            for (var i = 0; i < 6; i++)
            {
                coordinates.Keys.ToList().ForEach(c =>
                {
                    LinqExtension.DimensionalNeighbors(c.Split(',').Select(int.Parse))
                        .Select(t => string.Join(',', t))
                        .Where(t => !coordinates.ContainsKey(t))
                        .ToList()
                        .ForEach(t => coordinates[t] = false);
                });
                var coordinatesCopy = new Dictionary<string, bool>();

                coordinates.Keys.ToList().ForEach(c =>
                {
                    var activeNeighbors = LinqExtension.DimensionalNeighbors(c.Split(',').Select(int.Parse))
                        .Select(t => string.Join(',', t))
                        .Where(t => coordinates.ContainsKey(t))
                        .Select(t => coordinates[t])
                        .Count(v => v);
                    coordinatesCopy[c] = coordinates[c] switch
                    {
                        true when (activeNeighbors < 2 || activeNeighbors > 3) => false,
                        false when activeNeighbors == 3 => true,
                        _ => coordinates[c]
                    };
                });

                coordinates = coordinatesCopy;
            }

            return coordinates.Values.Count(v => v);
        }
    }
}
