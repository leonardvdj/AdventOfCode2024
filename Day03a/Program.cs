using System.Text.RegularExpressions;

namespace Day03a;

public static partial class Program
{
    public static void Main()
    {
        var input = string.Join(", ", File.ReadAllLines("input.txt"));

        var sum = MulRegex().Matches(input).Select(m =>
        {
            var nums = m.Groups.Values.Skip(1).Select(g => int.Parse(g.Value)).ToList();
            return nums[0] * nums[1];
        }).Sum();

        Console.WriteLine(sum);
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulRegex();
}
