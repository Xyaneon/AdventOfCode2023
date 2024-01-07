using Calculations;

using Domain;

namespace Extensions;

static class PuzzleInputLineExtensions
{
    public static HandType DetermineHandType(this PuzzleInputLine line) =>
        HandTypeDeterminer.DetermineHandType(line.Hand);
}
