// See https://aka.ms/new-console-template for more information

using AdventOfCode;
using AdventOfCode.DailyChallenges;
using AdventOfCode.DailyChallenges.Day02;

IChallenge challenge = new Challenge.Part2();

Console.Write("starting challenge: ");
Console.WriteLine(challenge.GetType().FullName);

var result = challenge.Execute();
result.Write(Console.Out);

Console.ReadLine();