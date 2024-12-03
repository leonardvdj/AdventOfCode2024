using System.Text.RegularExpressions;

var input = string.Join(", ", File.ReadAllLines("input.txt"));

var sum = Regex.Matches(input, @"mul\((\d+),(\d+)\)").Select(m =>
{
    var nums = m.Groups.Values.Skip(1).Select(g => int.Parse(g.Value)).ToList();
    return nums[0] * nums[1];
}).Sum();

Console.WriteLine(sum);
