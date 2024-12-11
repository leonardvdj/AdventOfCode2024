using System.Diagnostics;

namespace Day10b;

public static class Program
{
    public static void Main()
    {
        var matrix = ParseInput(File.ReadAllText("input.txt"));

        var trails = 0;

        // For every matrix cell that's 0, search for trails
        for (var y = 0; y < matrix.Length; y++)
        for (var x = 0; x < matrix[y].Length; x++)
        {
            var vec = new Vec2D(x, y);
            if (GetHeight(matrix, vec) != 0) continue;
            trails += GetTrailCount(matrix, vec);
        }

        Console.WriteLine(trails);
    }

    /// <summary>
    /// Searches for trails starting at the given <c>position</c>,
    /// and returns the number of complete trails found.
    /// </summary>
    /// <param name="matrix">The height matrix</param>
    /// <param name="position">The position to search from</param>
    /// <returns>The amount of complete trails found.</returns>
    private static int GetTrailCount(int[][] matrix, Vec2D position)
    {
        var nextPositions = GetSurroundingPositions(matrix, position);

        var trails = new HashSet<Vec2D>();

        while (nextPositions.Count > 0)
        {
            var next = nextPositions[0];
            nextPositions.RemoveAt(0);

            if (GetHeight(matrix, next) == 9) trails.Add(next);
            else nextPositions.AddRange(GetSurroundingPositions(matrix, next));
        }

        return trails.Count;
    }

    /// <summary>
    /// Parses the input into a 2D matrix of integers.
    /// </summary>
    /// <param name="input">The input text</param>
    /// <returns>An int matrix</returns>
    private static int[][] ParseInput(string input)
    {
        var lines = input.Split(["\r", "\n", "\r\n"], StringSplitOptions.RemoveEmptyEntries);
        var rows = new int[lines.Length][];

        for (var y = 0; y < lines.Length; y++)
        {
            rows[y] = new int[lines[y].Length];
            for (var x = 0; x < lines[y].Length; x++)
                rows[y][x] = int.Parse(lines[y][x].ToString());
        }

        return rows;
    }

    /// <summary>
    /// Gets the 2D offset of a given direction.
    /// </summary>
    /// <param name="direction">The direction</param>
    /// <returns>A <see cref="Program.Vec2D"/> containing the relative offset of the direction</returns>
    private static Vec2D GetDirectionOffset(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new(0, -1),
            Direction.Right => new(1, 0),
            Direction.Down => new(0, 1),
            Direction.Left => new(-1, 0),
            _ => throw new UnreachableException()
        };
    }

    /// <summary>
    /// Gets the height of a cell.
    /// </summary>
    /// <param name="matrix">The height matrix</param>
    /// <param name="position">The cell position</param>
    /// <returns>The height, or null if the position is outside the matrix bounds.</returns>
    private static int? GetHeight(int[][] matrix, Vec2D position)
    {
        if (position.Y < 0 || position.Y >= matrix.Length)
            return null;

        var row = matrix[position.Y];

        if (position.X < 0 || position.X >= row.Length)
            return null;

        return row[position.X];
    }

    /// <summary>
    /// Checks for cells in each direction with a value 1 higher than the current cell.
    /// </summary>
    /// <param name="matrix">The height matrix</param>
    /// <param name="position">The cell to check around</param>
    /// <returns>A list of positions surrounding <c>position</c> with a height increase of 1</returns>
    private static List<Vec2D> GetSurroundingPositions(int[][] matrix, Vec2D position)
    {
        var positions = new List<Vec2D>();
        var currentValue = GetHeight(matrix, position);
        if (currentValue == null) return [];

        for (var i = 0; i < Enum.GetNames(typeof(Direction)).Length; i++)
        {
            var offset = GetDirectionOffset((Direction)i);
            var nextPosition = position + offset;
            var adjacent = GetHeight(matrix, nextPosition);
            if (adjacent == null) continue;
            if (adjacent - currentValue == 1) positions.Add(nextPosition);
        }

        return positions;
    }

    /// <summary>
    /// A 2D position
    /// </summary>
    /// <param name="x">The x coordinate</param>
    /// <param name="y">The y coordinate</param>
    private class Vec2D(int x, int y)
    {
        public readonly int X = x;
        public readonly int Y = y;

        public Vec2D Clone()
        {
            return new(X, Y);
        }

        public static Vec2D operator +(Vec2D a)
        {
            return a;
        }

        public static Vec2D operator -(Vec2D a)
        {
            return new(-a.X, -a.Y);
        }

        public static Vec2D operator +(Vec2D a, Vec2D b)
        {
            return new(a.X + b.X, a.Y + b.Y);
        }

        public static Vec2D operator -(Vec2D a, Vec2D b)
        {
            return new(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2D operator *(Vec2D a, Vec2D b)
        {
            return new(a.X * b.X, a.Y * b.Y);
        }

        public static Vec2D operator /(Vec2D a, Vec2D b)
        {
            return new(a.X / b.X, a.Y / b.Y);
        }

        private bool Equals(Vec2D other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vec2D)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"{nameof(Vec2D)} {{ X = {X}, Y = {Y} }}";
        }
    }

    /// <summary>
    /// An enum of directions, ordered in clockwise fashion.
    /// </summary>
    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}
