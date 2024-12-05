namespace Day05a;

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllText("input.txt");
        var (dependencyLines, updateLines) = ParseInput(input);
        var pageDependencies = BuildDependencyGraph(dependencyLines);
        var updates = updateLines.Select(line => line.Split(",")).ToList();

        var sum = 0;

        foreach (var update in updates)
        {
            var isCorrect = true;
            for (var i = 0; i < update.Length; i++)
            {
                var page = update[i];
                var dependencies = pageDependencies.GetValueOrDefault(page, []);
                var pagesAfter = update[(i + 1)..];

                if (dependencies.Intersect(pagesAfter).Any())
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                var middle = update.Length / 2;
                sum += int.Parse(update[middle]);
            }
        }

        Console.WriteLine(sum);
    }

    private static (List<string>, List<string>) ParseInput(string input)
    {
        var lines = input.Split(["\r", "\n", "\r\n"], StringSplitOptions.None);
        var hasReachedUpdates = false;
        var dependencies = new List<string>();
        var updates = new List<string>();

        foreach (var line in lines)
        {
            if (line == "")
            {
                hasReachedUpdates = true;
                continue;
            }

            if (hasReachedUpdates) updates.Add(line);
            else dependencies.Add(line);
        }

        return (dependencies, updates);
    }

    /// <summary>
    /// Builds a dependency graph where the key is a page,
    /// and the value is a list of pages it depends on.
    /// </summary>
    /// <param name="dependencies">A list of dependency pairs in format of dependency|page</param>
    private static Dictionary<string, List<string>> BuildDependencyGraph(List<string> dependencies)
    {
        return dependencies
            .Select(line => line.Split("|"))
            .GroupBy(pair => pair[1])
            .ToDictionary(g => g.Key, g => g.Select(p => p[0]).ToList());
    }
}
