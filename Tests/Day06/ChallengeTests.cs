using System.Numerics;
using AdventOfCode.DailyChallenges.Day06;
using NUnit.Framework;
using AdventOfCode.Lib;

namespace Tests.Day06;

public class ChallengeTests
{
    [Test]
    public void Challenge_MatchesExample()
    {
        var input = "3,4,3,1,2";
        var ch = new Challenge.Part1();
        var res = (ValueChallengeResult<BigInteger>)ch.Execute(input);
        Assert.AreEqual(5934, res.Value);
    }
}