namespace Day01b;

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllLines("input.txt")
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();

        var leftIdSimilarityScores = input.Select(ids => ids[0]).ToDictionary(id => id, _ => 0);
        var rightIdCounts = input.Select(ids => ids[1]).GroupBy(id => id).ToDictionary(g => g.Key, g => g.Count());

        foreach (var (id, _) in leftIdSimilarityScores)
            leftIdSimilarityScores[id] = rightIdCounts.GetValueOrDefault(id, 0);

        var similarityScore = leftIdSimilarityScores.Sum(kvp => kvp.Key * kvp.Value);

        Console.WriteLine(similarityScore);
    }
}
