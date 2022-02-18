using AdventOfCode.Lib;

namespace AdventOfCode;

public interface IChallenge
{
    string DefaultInput { get; }
    ChallengeResult Execute(string? input = null);
}