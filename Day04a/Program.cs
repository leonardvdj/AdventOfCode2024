namespace Day04a;

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllLines("input.txt");
        const string target = "XMAS";

        var count = 0;

        for (var y = 0; y < input.Length; y++)
        for (var x = 0; x < input[y].Length; x++)
        {
            var width = input[y].Length;

            var letter = input[y][x];
            if (letter != target[0]) continue;

            for (var dirX = -1; dirX <= 1; dirX++)
            for (var dirY = -1; dirY <= 1; dirY++)
            {
                if (dirX == 0 && dirY == 0) continue;
                for (var step = 1; step < target.Length; step++)
                {
                    var nextX = x + dirX * step;
                    var nextY = y + dirY * step;
                    if (nextX < 0 || nextX >= width || nextY < 0 || nextY >= input.Length) break;
                    var next = input[nextY][nextX];
                    if (next != target[step]) break;
                    if (step == target.Length - 1)
                    {
                        count++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(count);
    }
}
