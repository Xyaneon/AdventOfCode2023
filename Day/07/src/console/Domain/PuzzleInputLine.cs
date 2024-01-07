namespace Domain;

public class PuzzleInputLine : IComparable<PuzzleInputLine>
{
    public PuzzleInputLine(Hand hand, int bid)
    {
        Hand = hand ?? throw new ArgumentNullException(nameof(hand));
        Bid = bid;
    }

    public Hand Hand { get; init; }

    public int Bid { get; init; }

    public int CompareTo(PuzzleInputLine? other) =>
        other is null ? 0 : Hand.CompareTo(other.Hand);
}
