using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day10
{
    // What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device?
    public class SecondSolver : ISolver
    {
        private readonly IDictionary<string, long> _cache = new Dictionary<string, long>();
        private long GetBranchesAmount(IList<long> adapters)
        {
            var cacheKey = string.Join(',', adapters);
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache[cacheKey];
            }

            long result = 1;

            if (adapters.Count > 1)
            {
                var firstAdapter = adapters[0];
                result = 0;
                for (var i = 0; i < 3; i++)
                {
                    if (adapters.Contains(firstAdapter + i + 1))
                    {
                        result += GetBranchesAmount(adapters.SkipWhile(x => x < firstAdapter + i + 1).ToList());
                    }
                }
            }
            _cache[cacheKey] = result;
            return result;
        }
        
        public long Calculate(List<string> input)
        {
            var adapters = input.Select(long.Parse).OrderBy(x => x).ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.Max() + 3);

            var res = GetBranchesAmount(adapters);
            return res;
        }
    }
}
