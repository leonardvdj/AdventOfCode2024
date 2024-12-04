namespace Day01b;

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllLines("input.txt")
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();

        var rightIdCounts = input.Select(ids => ids[1]).GroupBy(id => id).ToDictionary(g => g.Key, g => g.Count());

        var similarityScore = 0;

        foreach (var idPair in input)
        {
            var leftId = idPair[0];
            var count = rightIdCounts.GetValueOrDefault(leftId, 0);
            similarityScore += leftId * count;
        }

        Console.WriteLine(similarityScore);
    }
}
