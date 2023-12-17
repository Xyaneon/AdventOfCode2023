static class ScratchcardExtensions
{
    public static int CalculatePoints(this Scratchcard scratchcard)
    {
        int numberOfMatches = scratchcard.DetermineMatchingNumbers()
            .Count();

        return numberOfMatches > 0 ? (int) Math.Pow(2, numberOfMatches - 1) : 0;
    }

    public static IEnumerable<int> DetermineMatchingNumbers(this Scratchcard scratchcard) =>
        scratchcard.WinningNumbers
            .Where(winningNumber => scratchcard.NumbersYouHave.Contains(winningNumber));
}
