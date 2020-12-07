using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day7
{
    // How many individual bags are required inside your single shiny gold bag?
    public class SecondSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var bags = input.Select(l => new Bag(l)).ToList();
            return Bag.GetBag(Bag.ShinyGoldName, bags).CalculateBags(bags);
        }
    }
}