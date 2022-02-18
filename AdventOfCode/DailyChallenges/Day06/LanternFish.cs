namespace AdventOfCode.DailyChallenges.Day06;

public record struct LanternFish(int DaysToReproduce)
{
    public static int ReproductionDaysAfterBirth { get; } = 8;
    public static int ReproductionDaysReset{ get; } = 6;

    public (LanternFish original, LanternFish? child) Tick()
    {
        var newDays = DaysToReproduce - 1;
        if (newDays == -1)
        {
            // had a baby
            return (new LanternFish(ReproductionDaysReset), new LanternFish(ReproductionDaysAfterBirth));
        }

        return (new LanternFish(newDays), null);
    }
}