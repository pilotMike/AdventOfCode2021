using AdventOfCode.Lib;

namespace AdventOfCode.DailyChallenges.Day02;

public enum Direction { Forward, Down, Up }

public readonly record struct Command(Direction Direction, int Distance);

public readonly record struct Depth(int Value);

public readonly record struct Location(Point2D Coordinate)
{
    /// <summary>
    /// Depth is the inversion of the vertical distance
    /// </summary>
    public int Depth => -Coordinate.Y;

    public static Location operator +(Location l, Depth d) =>
        new Location(new Point2D(l.Coordinate.X, l.Coordinate.Y - d.Value));

    public static Location operator +(Location l, Point2D delta) =>
        new(new(l.Coordinate.X + delta.X, l.Coordinate.Y + delta.Y));
}

public readonly record struct Point2D(int X, int Y)
{
    public static implicit operator Point2D((int x, int y) tuple) =>
        new Point2D(tuple.x, tuple.y);

    public static Point2D operator +(Point2D a, Point2D b) => new Point2D(a.X + b.X, a.Y + b.Y);
    public static Point2D operator +(Point2D point, Depth d) => new(point.X, point.Y - d.Value);
}