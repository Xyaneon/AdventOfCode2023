using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Domain;

static class InputParser
{
    private const string NetworkLineRegexPattern = @"([0-9a-zA-Z]+)\s*=\s*\(([0-9a-zA-Z]+),\s*([0-9a-zA-Z]+)\)";
    private static readonly Regex NetworkLineRegex = new(NetworkLineRegexPattern);

    public static PuzzleInput ParseAsPuzzleInput(IList<string> lines)
    {
        ImmutableList<Direction> directions = ParseDirections(lines[0]);

        IEnumerable<NetworkLine> networkLines = lines.Skip(2)
                                                     .Select(line => ParseNetworkLine(line));
        Network network = new(networkLines);

        return new PuzzleInput(directions, network);
    }

    private static ImmutableList<Direction> ParseDirections(string line) =>
        line.Select(chr => ParseDirection(chr)).ToImmutableList();

    private static Direction ParseDirection(char chr) => chr switch
        {
            'L' => Direction.Left,
            'R' => Direction.Right,
            _ => throw new ArgumentException($"Unsupported direction character '{chr}'"),
        };
    
    private static NetworkLine ParseNetworkLine(string line)
    {
        var match = NetworkLineRegex.Match(line);

        string sourceNode = match.Groups[1].Value;
        string leftNode = match.Groups[2].Value;
        string rightNode = match.Groups[3].Value;

        return new(sourceNode, leftNode, rightNode);
    }
}
