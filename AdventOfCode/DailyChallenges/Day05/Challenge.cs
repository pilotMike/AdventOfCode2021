using AdventOfCode.Lib;

namespace AdventOfCode.DailyChallenges.Day05;

public static class Challenge
{
    
    public class Part1 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;

            var lines = Parse(input)
                .Where(line => line.IsHorizontal || line.IsVertical);

            var pointMap = new PointMap(lines);
            var overlaps = pointMap.Count(kvp => kvp.Value > 1);

            return new ValueChallengeResult<int>(overlaps);
        }

    }

    public class Part2 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;

            var lines = Parse(input);
            var pointMap = new PointMap(lines);
            var overlaps = pointMap.Count(kvp => kvp.Value > 1);

            return new ValueChallengeResult<int>(overlaps);
        }
    }
    
    
    private static IEnumerable<Line> Parse(string input) =>
        input.SplitParse(line =>
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
}