using System.Numerics;
using AdventOfCode.Lib;

namespace AdventOfCode.DailyChallenges.Day07;

public static class Challenge
{
    public class Part1 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;

            var positions = input.SplitParse(",")
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            var min = positions.Keys.Min();
            var max = positions.Keys.Max();

            var leastFuel = Enumerable.Range(min, max - min)
                .Min(position =>
                {
                    var consumed = FuelConsumed(position, positions);
                    //Console.WriteLine("position {0} used {1} fuel", position, consumed);
                    return consumed;
                });

            return new ValueChallengeResult<int>(leastFuel);
        }

        private static int FuelConsumed(int targetPosition, IReadOnlyDictionary<int, int> positions) =>
            positions
                .Sum(kvp =>
                {
                    var distance = Math.Abs(targetPosition - kvp.Key);
                    return distance * kvp.Value;
                });
    }

    public class Part2 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;
            
            var positions = input.SplitParse(",")
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            var min = positions.Keys.Min();
            var max = positions.Keys.Max();

            var leastFuel = Enumerable.Range(min, max - min)
                .Min(position =>
                {
                    var consumed = FuelConsumed(position, positions);
                    //Console.WriteLine("position {0} used {1} fuel", position, consumed);
                    return consumed;
                });

            return new ValueChallengeResult<BigInteger>(leastFuel);
        }

        private static BigInteger FuelConsumed(int targetPosition, IReadOnlyDictionary<int, int> positions) =>
            positions.Aggregate(BigInteger.Zero, (acc, kvp) =>
            {
                BigInteger distance = Math.Abs(targetPosition - kvp.Key);
                var fuelUsed = Numbers.NaturalSum(distance) * kvp.Value;
                return acc + fuelUsed;
            });
    }
}