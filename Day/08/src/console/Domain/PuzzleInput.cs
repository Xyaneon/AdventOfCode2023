using System.Collections.Immutable;

namespace Domain;

public class PuzzleInput
{
    public PuzzleInput(IEnumerable<Direction> directions, Network network)
    {
        Directions = directions.ToImmutableList();
        Network = network;
    }
    
    public ImmutableList<Direction> Directions { get; init; }

    public Network Network { get; init; }

    public override string ToString()
    {
        var parts = new string[] { GetDirectionsAsString(), "", Network.ToString() };
        return string.Join(Environment.NewLine, parts);
    }

    private string GetDirectionsAsString() =>
        string.Join("", Directions.Select(direction => direction == Direction.Left ? 'L' : 'R'));
}
