using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7
{
    public class Bag
    {
        public static string ShinyGoldName = "shiny gold";
        private static readonly Regex ColorMatcherRegex = new Regex(@"^[^\s]+\s+[^\s]+", RegexOptions.Compiled);
        private static readonly Regex BagsMatcherRegex = new Regex(@"(\d+) ([^\s]+\s+[^\s]+)", RegexOptions.Compiled);
        public readonly string Name;
        public readonly IDictionary<string, int> Content;

        public Bag(string line)
        {
            Name = ColorMatcherRegex.Match(line).Value;
            Content = BagsMatcherRegex.Matches(line).ToList()
                .ToDictionary(
                    m => m.Groups[2].Value,
                    m => int.Parse(m.Groups[1].Value)
                );
        }

        private Bag GetBag(string key, List<Bag> allBags)
        {
            return allBags.First(b => b.Name.Equals(key));
        }

        public bool ContainsBag(string name, List<Bag> allBags)
        {
            return Content.Keys.Any(key => key.Equals(name) || GetBag(key, allBags).ContainsBag(name, allBags));
        }

        public long CalculateBags(List<Bag> allBags)
        {
            return Content.Sum(kv =>
                    kv.Value * (1 + GetBag(kv.Key, allBags).CalculateBags(allBags))
                );
        }
    }
}