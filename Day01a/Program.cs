var input = File.ReadAllLines("input.txt")
    .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();

var leftIds = input.Select(ids => ids[0]).Order().ToList();
var rightIds = input.Select(ids => ids[1]).Order().ToList();

var totalDistance = 0;

for (var i = 0; i < input.Count; i++)
    totalDistance += Math.Abs(leftIds[i] - rightIds[i]);

Console.WriteLine(totalDistance);
