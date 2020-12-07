using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day7
{
    // How many bag colors can eventually contain at least one shiny gold bag?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var bags = input.Select(l => new Bag(l)).ToList();
            return bags.Count(b => b.ContainsBag(Bag.ShinyGoldName, bags));
        }
    }
}