using System.Text.RegularExpressions;

var input = string.Join(", ", File.ReadAllLines("input.txt"));

var matches = Regex.Matches(input, @"(?:mul\((\d+),(\d+)\))|(?:do(?:n't)?\(\))");

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
