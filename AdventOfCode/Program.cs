using AdventOfCode;
using AdventOfCode.DailyChallenges.Day05;

IChallenge challenge = new Challenge.Part2();

Console.Write("starting challenge: ");
Console.WriteLine(challenge.GetType().FullName);

var result = challenge.Execute();
result.Write(Console.Out);

Console.ReadLine();