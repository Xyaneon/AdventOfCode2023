using Calculations;

using Domain;

namespace Extensions;

static class PuzzleInputLineExtensions
{
    public static HandType DetermineHandType(this PuzzleInputLine line) =>
        line.Hand.GetHandType();
}
