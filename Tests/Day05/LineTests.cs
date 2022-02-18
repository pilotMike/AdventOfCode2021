using AdventOfCode.DailyChallenges.Day05;
using AdventOfCode.Lib;
using NUnit.Framework;

namespace Tests.Day05;

public class LineTests
{
    [Test]
    public void EnumeratesTheCorrectPoints()
    {
        var count = new Line((0, 9), (5, 9)).Count();
        Assert.AreEqual(6, count);
    }

    [Test]
    public void PointMap_GetsCorrectPoints()
    {
        var lines = Parse(input);
        var pointMap = new PointMap(lines);
        var overlaps = pointMap.Count(x => x.Value > 1);
        Assert.AreEqual(5, overlaps);
    }

    [TestCase(770,318, 770,437, 120)]
    [TestCase(132,311, 132,89, 223)]
    public void MoreLineEnumerationTests(int x1, int y1, int x2, int y2, int expected)
    {
        var l = new Line((x1, y1), (x2, y2));
        
        var count = l.Count();
        Assert.AreEqual(expected, count);
    }

    [TestCase(1,1,3,3, 3)]
    [TestCase(9,7,7,9, 3)]
    public void DiagonalLineTests(int x1, int y1, int x2, int y2, int expected)
    {
        var l = new Line((x1, y1), (x2, y2));
        
        var count = l.Count();
        Assert.AreEqual(expected, count);
    }

    [Test]
    public void FullDiagonalLineTest()
    {
        var count = new PointMap(Parse(FullInput)).Count(x => x.Value > 1);
        Assert.AreEqual(12, count);
    }
    
    
    
    // 770,318 -> 770,437
    // 132,311 -> 132,89
    // 926,479 -> 926,37
    // 239,395 -> 239,722
    // 286,538 -> 713,538
    // 216,945 -> 570,945
    // 975,858 -> 854,858
    // 846,437 -> 313,437
    // 90,318 -> 90,151
    private IEnumerable<Line> Parse(string input) => input.SplitParse(line =>
    {
        var i = line.IndexOf(',');
        var x1 = int.Parse(line[..i]);
        var y1 = int.Parse(line[(i+1)..line.IndexOf(' ')]);
        i = line.LastIndexOf(' ');
        line = line.Slice(i);
        var next = line.IndexOf(',');
        var x2 = int.Parse(line[..next]);
        var y2 = int.Parse(line[(next+1)..]);

        var start = new Point2D(x1, y1);
        var end = new Point2D(x2, y2);
        return new Line(start, end);
    });

    private const string input = @"0,9 -> 5,9
9,4 -> 3,4
7,0 -> 7,4
0,9 -> 2,9
3,4 -> 1,4";

    private const string FullInput = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";
}