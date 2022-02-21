using AdventOfCode.DailyChallenges.Day07;
using AdventOfCode.Lib;
using NUnit.Framework;

namespace Tests.Day07;

public class Tests
{
    [Test]
    public void ArrivesToCorrectAnswer()
    {
        const string crabPositions = "16,1,2,0,4,2,7,1,2,14";
        var leastFuelUsed = (ValueChallengeResult<int>) new Challenge.Part1().Execute(crabPositions);
        Assert.AreEqual(37, leastFuelUsed.Value);
    }

    [Test]
    public void Part2()
    {
        const string crabPositions = "16,1,2,0,4,2,7,1,2,14";
        var leastFuelUsed = (ValueChallengeResult<int>) new Challenge.Part2().Execute(crabPositions);
        Assert.AreEqual(168, leastFuelUsed.Value);
    }
}