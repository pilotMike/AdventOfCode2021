using AdventOfCode.Lib;
using MoreLinq;

namespace AdventOfCode.DailyChallenges.Day01;

public static class Challenge 
{
    public class Part1 : IChallenge
    {
        public string DefaultInput => Input.Part1;

        public ChallengeResult Execute(string? input = null)
        {
            if (input == null) input = DefaultInput;

            var count = input.SplitParse()
                .Pairwise((first, second) => first < second)
                .Count(b => b);

            return new ValueChallengeResult<int>(count);
        }
    }

    public class Part2 : IChallenge
    {
        public string DefaultInput => Input.Part1;
        public ChallengeResult Execute(string? input = null)
        {
            if (input == null) input = DefaultInput;

            var count = input.SplitParse()
                .WindowLeft(3)
                //.Where(window  => window.Count == 3)
                .Pairwise((left, right) => left.Sum() < right.Sum())
                .Count(b => b);

            return new ValueChallengeResult<int>(count);
        }
    }
}