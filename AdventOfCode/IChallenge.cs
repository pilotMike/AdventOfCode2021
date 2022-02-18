using System.Runtime.CompilerServices;
using AdventOfCode.Lib;

[assembly:InternalsVisibleTo("Tests")]
namespace AdventOfCode;

public interface IChallenge
{
    string DefaultInput { get; }
    ChallengeResult Execute(string? input = null);
}