// See https://aka.ms/new-console-template for more information

using AdventOfCode;
using AdventOfCode.DailyChallenges.Day01;

IChallenge challenge = new Challenge.Part2();

Console.Write("starting challenge: ");
Console.WriteLine(challenge.GetType().FullName);

var result = challenge.Execute();
result.Write(Console.Out);

Console.ReadLine();