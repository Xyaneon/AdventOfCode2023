﻿using System.Collections.Immutable;
using System.Diagnostics;

using Domain;

using Extensions;

if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day7.exe input-file");
    return 1;
}

string[] lines;

try
{
    lines = File.ReadAllLines(args[0]);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Failed to read input file: {ex.Message}");
    return 2;
}

var stopwatch = Stopwatch.StartNew();

ImmutableList<PuzzleInputLine> sortedPuzzleInputLines = lines
    .Select(line => line.ParseAsPuzzleInputLine())
    .OrderBy(line => line)
    .ToImmutableList();

var score = 0;
for (int i = 0; i < sortedPuzzleInputLines.Count; i++)
{
    var line = sortedPuzzleInputLines[i];
    var rank = i + 1;
    var handScore = line.Bid * rank;
    score += handScore;
    Console.WriteLine($"Rank {rank}: {line.Hand} {line.Bid} {line.DetermineHandType()} Score: {line.Bid} * {rank} = {handScore}");
}

stopwatch.Stop();

Console.WriteLine("---");
Console.WriteLine($"Total score: {score} (found in {stopwatch.ElapsedMilliseconds} ms)");

return 0;
