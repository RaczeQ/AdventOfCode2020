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
