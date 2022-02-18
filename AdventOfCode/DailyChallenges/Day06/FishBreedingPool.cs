using System.Numerics;

namespace AdventOfCode.DailyChallenges.Day06;

public class FishBreedingPool
{
    private Dictionary<int, BigInteger> _countOfFishByDaysForBirth = new();
    public FishBreedingPool(IEnumerable<LanternFish> fish)
    {
        foreach (var f in fish)
        {
            if (_countOfFishByDaysForBirth.TryGetValue(f.DaysToReproduce, out var count))
                _countOfFishByDaysForBirth[f.DaysToReproduce] = count + 1;
            else _countOfFishByDaysForBirth[f.DaysToReproduce] = 1;
        }
    }

    public FishBreedingPool Tick()
    {
        var fishes = _countOfFishByDaysForBirth
            .Select(kvp => (fish:new LanternFish(kvp.Key), count:kvp.Value))
            .Select(fishes =>
            {
                var (original, child) = fishes.fish.Tick();
                if (child.HasValue)
                    return (original.DaysToReproduce, child:(int?)child.Value.DaysToReproduce, count: fishes.count);
                return (original.DaysToReproduce, child: new int?(), count: fishes.count);
            });

        var newPool = new Dictionary<int, BigInteger>();
        foreach (var (daysToReproduce, child, count) in fishes)
        {
            SetCount(newPool, daysToReproduce, count);

            if (child.HasValue)
                SetCount(newPool, child.Value, count);
        }

        var pool = new FishBreedingPool(Enumerable.Empty<LanternFish>());
        pool._countOfFishByDaysForBirth = newPool;
        return pool;
    }

    public BigInteger TotalFish() => _countOfFishByDaysForBirth.Values.Aggregate(BigInteger.Zero, (acc, next) => acc + next);

    private static void SetCount(Dictionary<int, BigInteger> newPool, int daysToReproduce, BigInteger count)
    {
        if (newPool.TryGetValue(daysToReproduce, out var originalCount))
            newPool[daysToReproduce] = originalCount + count;
        else
            newPool[daysToReproduce] = count;
    }
}