namespace Domain;

public class PuzzleInputLine
{
    public PuzzleInputLine(Hand hand, int bid)
    {
        Hand = hand ?? throw new ArgumentNullException(nameof(hand));
        Bid = bid;
    }

    public Hand Hand { get; init; }

    public int Bid { get; init; }
}
