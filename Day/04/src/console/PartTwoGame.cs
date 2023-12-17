class PartTwoGame
{
    public PartTwoGame(IEnumerable<Scratchcard> scratchcards)
    {
        ScratchcardCopies = scratchcards.Select(card => new CopyableScratchcard(card))
            .ToList();
        WasPlayed = false;
    }

    public bool WasPlayed { get; private set; }

    public List<CopyableScratchcard> ScratchcardCopies { get; init; }

    public void Play()
    {
        if (WasPlayed)
        {
            throw new InvalidOperationException("This game was already played.");
        }

        for (int i = 0; i < ScratchcardCopies.Count; i++)
        {
            var cardsBeingScored = ScratchcardCopies.ElementAt(i);
            var numberOfMatches = cardsBeingScored.Scratchcard.DetermineMatchingNumbers().Count();

            if (numberOfMatches > 0)
            {
                for (int j = i + 1; j <= i + numberOfMatches; j++)
                {
                    if (j < ScratchcardCopies.Count)
                    {
                        int copiesToAdd = cardsBeingScored.Copies;
                        ScratchcardCopies.ElementAt(j).AddCopies(copiesToAdd);
                    }
                }
            }
        }

        WasPlayed = true;
    }

    public int CountTotalNumberOfScratchcards() =>
        ScratchcardCopies.Select(card => card.Copies).Sum();
}
