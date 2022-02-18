using System.Numerics;
using AdventOfCode.Lib;

namespace AdventOfCode.DailyChallenges.Day06;

public static class Challenge
{
    public class Part1 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;

            var res = CountFishAfter(80, input);
            return new ValueChallengeResult<BigInteger>(res);
        }
    }

    public class Part2 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;
            var res = CountFishAfter(256, input);
            return new ValueChallengeResult<BigInteger>(res);
        }
    }

    private static BigInteger CountFishAfter(int days, string input)
    {
        var fish = input.SplitParse(s => new LanternFish(int.Parse(s)), ",");
        var fishPool = new FishBreedingPool(fish);

        for (int i = 0; i < days; i++)
        {
            fishPool = fishPool.Tick();
        }

        return fishPool.TotalFish();
    }
}