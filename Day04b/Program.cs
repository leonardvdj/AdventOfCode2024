namespace Day04b;

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllLines("input.txt");

        var count = 0;

        for (var y = 0; y < input.Length; y++)
        for (var x = 0; x < input[y].Length; x++)
        {
            var width = input[y].Length;

            if (x < 1 || x >= width - 1 || y < 1 || y >= input.Length - 1) continue;
            var letter = input[y][x];
            if (letter != 'A') continue;

            var diag1 = Enumerable.Range(0, 3).Select(i => input[y - 1 + i][x - 1 + i]).ToList();
            var diag1Correct = diag1.Contains('M') && diag1.Contains('S');
            var diag2 = Enumerable.Range(0, 3).Select(i => input[y + 1 - i][x - 1 + i]).ToList();
            var diag2Correct = diag2.Contains('M') && diag2.Contains('S');

            if (diag1Correct && diag2Correct) count++;
        }

        Console.WriteLine(count);
    }
}
