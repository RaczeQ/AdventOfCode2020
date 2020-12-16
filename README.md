# AdventOfCode2020

**Language**: C#  
**Framework**: .NET 5.0

These are my [Advent of Code](https://adventofcode.com) (AoC) solutions for a year 2020.

Solutions aren't written to be as efficient as possible, but I try to use mainly LINQ queries to calculate an answer.

### Console

Project is interacted using a console menu (visible below).  
Console reads input data from either a text file or by pasting it directly into a console.

![](https://media.giphy.com/media/QVEsk25PfUvheWt0me/giphy.gif)

You can find it coded in [ElvenTools.IO.ConsoleMenu](ElvenTools/IO/ConsoleMenu.cs)

### LINQ Examples

Few pure LINQ puzzles solutions:

**Day 1 - Part 1**
```C#
public long Calculate(List<string> input)
{
    var numbers = input.Select(int.Parse).ToList();
    return numbers
        .SelectMany((x, i) => numbers.Skip(i + 1), (x, y) => (x, y))
        .Where(t => t.x + t.y == 2020)
        .Select(t => t.x * t.y)
        .First();
}
```

**Day 6 - Part 2**
```C#
public long Calculate(List<string> input)
{
    return string.Join("\n", input)
        .Split("\n\n")
        .Select(x => x.Split("\n").Select(l => l.ToCharArray().Distinct()))
        .Select(g => g.Aggregate((prev, next) => prev.Intersect(next).ToList()).Count())
        .Sum();
}
```

**Day 9 - Part 1** (finding pair sum is copied Day 1 - Part 1)
```C#
public long Calculate(List<string> input)
{
    const int preambleSize = 25;
    var numbers = input.Select(long.Parse).ToList();
    return numbers
        .Skip(preambleSize)
        .Select((x, i) => (x, numbers.Skip(i).Take(preambleSize).ToList()))
        .First(t => t.Item2
            .SelectMany((x, i) => t.Item2.Skip(i + 1), (x, y) => (x, y))
            .All(p => p.x + p.y != t.x)).x;
}
```

**Day 13 - Part 1**
```C#
public long Calculate(List<string> input)
{
    var timestamp = long.Parse(input[0]);
    return input[1]
        .Split(',')
        .Where(c => !c.Equals("x"))
        .Select(long.Parse)
        .Select(b => (b, Convert.ToInt64(b * Math.Ceiling((float) timestamp / (float) b) - timestamp)))
        .OrderBy(t => t.Item2)
        .Select(t => t.b * t.Item2)
        .First();
}
```

**Day 16 - Part 1**
```C#
public long Calculate(List<string> input)
{
    var ruleRegex = new Regex(@"(.*): (\d*)-(\d*) or (\d*)-(\d*)", RegexOptions.Compiled);
    var yourTicketIdx = input.FindIndex(l => l.StartsWith("your ticket"));
    var nearbyTicketIdx = input.FindIndex(l => l.StartsWith("nearby tickets"));

    var rules = input
        .Take(yourTicketIdx - 1)
        .Select(l => ruleRegex.Match(l).Groups)
        .SelectMany(g => new [] {(int.Parse(g[2].Value), int.Parse(g[3].Value)),
            (int.Parse(g[4].Value), int.Parse(g[5].Value))
        }).ToList();
    return input
        .Skip(nearbyTicketIdx + 1)
        .SelectMany(l => l.Split(','))
        .Select(int.Parse)
        .Where(n => rules.All(t => n < t.Item1 || n > t.Item2))
        .Sum();
}
```
