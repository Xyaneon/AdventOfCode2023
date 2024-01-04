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

List<PuzzleInputLine> puzzleInputLines = lines.Select(line => line.ParseAsPuzzleInputLine())
                                              .ToList();

foreach (PuzzleInputLine line in puzzleInputLines)
{
    Console.WriteLine($"{line.Hand} {line.Bid}");
}

// TODO: Fill in the rest.

return 0;
