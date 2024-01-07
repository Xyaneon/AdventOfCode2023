using System.Diagnostics;

using Calculations;

using Domain;

if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day8.exe input-file");
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

PuzzleInput puzzleInput = InputParser.ParseAsPuzzleInput(lines);
Console.WriteLine(puzzleInput);

Console.WriteLine("---");

var stopwatch = Stopwatch.StartNew();

List<string> startNodes = puzzleInput.Network.GetPartTwoStartNodes().ToList();

Console.WriteLine($"{startNodes.Count} start nodes identified.");

long steps = GhostNavigator.GetStepsToNavigate(puzzleInput);

stopwatch.Stop();

Console.WriteLine($"End state reached in {steps} steps after {stopwatch.ElapsedMilliseconds} ms.");

return 0;
