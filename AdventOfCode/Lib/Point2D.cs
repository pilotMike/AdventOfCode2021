using AdventOfCode.DailyChallenges.Day02;

namespace AdventOfCode.Lib;

public readonly record struct Point2D(int X, int Y)
{
    public static implicit operator Point2D((int x, int y) tuple) =>
        new Point2D(tuple.x, tuple.y);

    public static Point2D operator +(Point2D a, Point2D b) => new Point2D(a.X + b.X, a.Y + b.Y);
    public static Point2D operator +(Point2D point, Depth d) => new(point.X, point.Y - d.Value);
}