using System.Collections;
using AdventOfCode.Lib;

namespace AdventOfCode.DailyChallenges.Day05;

public class PointMap : IEnumerable<KeyValuePair<Point2D, int>>
{
    private readonly Dictionary<Point2D, int> map = new ();

    public PointMap(IEnumerable<Line> lines)
    {
        foreach (var line in lines)
            Add(line);
    }

    public void Add(Line line)
    {
        foreach (var point in line)
        {
            if (map.TryGetValue(point, out var count))
                map[point] = count + 1;
            else
                map[point] = 1;
        }
    }

    public Dictionary<Point2D, int>.Enumerator GetEnumerator() => map.GetEnumerator();

    IEnumerator<KeyValuePair<Point2D, int>> IEnumerable<KeyValuePair<Point2D, int>>.GetEnumerator() => map.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => map.GetEnumerator();
}