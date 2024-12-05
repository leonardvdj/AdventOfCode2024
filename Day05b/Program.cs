namespace Day05b;

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllText("input.txt");
        var (dependencyLines, updateLines) = ParseInput(input);
        var pageDependencies = BuildDependencyGraph(dependencyLines);
        var updates = updateLines.Select(line => line.Split(",")).ToList();

        var incorrectUpdates = FindIncorrectUpdates(pageDependencies, updates);
        var sum = 0;

        foreach (var update in incorrectUpdates)
        {
            for (var i = update.Length - 2; i >= 0; i--)
            {
                var page = update[i];
                var dependencies = pageDependencies.GetValueOrDefault(page, []);
                if (dependencies.Count == 0) continue;

                // Search end-to-start to find the back-most unfulfilled dependency
                for (var j = update.Length - 1; j > i; j--)
                {
                    // If the current `page` has a dependency that is ahead of this one,
                    // we have to move it behind that page
                    if (dependencies.Contains(update[j]))
                    {
                        // Loop through every page ahead and shift it 1 lower
                        for (var k = i + 1; k <= j; k++)
                            update[k - 1] = update[k];

                        // Place the page in the newly freed spot,
                        // and break since it is the back-most dependency
                        update[j] = page;
                        break;
                    }
                }
            }

            var middle = update.Length / 2;
            sum += int.Parse(update[middle]);
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

    private static List<string[]> FindIncorrectUpdates(Dictionary<string, List<string>> pageDependencies,
        List<string[]> updates)
    {
        var incorrectUpdates = new List<string[]>();

        foreach (var update in updates)
        {
            for (var i = 0; i < update.Length; i++)
            {
                var page = update[i];
                var dependencies = pageDependencies.GetValueOrDefault(page, []);
                var pagesAfter = update[(i + 1)..];

                if (dependencies.Intersect(pagesAfter).Any())
                {
                    incorrectUpdates.Add(update);
                    break;
                }
            }
        }

        return incorrectUpdates;
    }
}
