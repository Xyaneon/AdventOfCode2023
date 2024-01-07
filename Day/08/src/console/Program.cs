using System.Diagnostics;

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

string currentNode = NodeConstants.StartNode;
int steps = 0;
int directionIndex = 0;

while (currentNode != NodeConstants.EndNode)
{
    Direction nextDirection = puzzleInput.Directions[directionIndex];

    currentNode = nextDirection == Direction.Left
        ? puzzleInput.Network.GetLeftNode(currentNode)
        : puzzleInput.Network.GetRightNode(currentNode);
    
    steps++;
    directionIndex++;

    if (directionIndex >= puzzleInput.Directions.Count)
        directionIndex = 0;
}

stopwatch.Stop();

Console.WriteLine($"End state reached in {steps} steps after {stopwatch.ElapsedMilliseconds} ms.");

return 0;
