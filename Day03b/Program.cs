using System.Text.RegularExpressions;

namespace Day03b;

public static partial class Program
{
    public static void Main()
    {
        var input = string.Join(", ", File.ReadAllLines("input.txt"));

        var matches = MulRegex().Matches(input);

        var sum = 0;
        var process = true;
        foreach (Match match in matches)
            switch (match.Value)
            {
                case "do()":
                    process = true;
                    break;
                case "don't()":
                    process = false;
                    break;
                default:
                    if (process)
                    {
                        var nums = match.Groups.Values.Skip(1).Select(g => int.Parse(g.Value)).ToList();
                        sum += nums[0] * nums[1];
                    }

                    break;
            }

        Console.WriteLine(sum);
    }

    [GeneratedRegex(@"(?:mul\((\d+),(\d+)\))|(?:do(?:n't)?\(\))")]
    private static partial Regex MulRegex();
}
