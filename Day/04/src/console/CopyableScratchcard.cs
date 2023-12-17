class CopyableScratchcard
{
    public CopyableScratchcard(Scratchcard scratchcard)
    {
        Scratchcard = scratchcard;
        Copies = 1;
    }

    public int Copies { get; private set; }

    public Scratchcard Scratchcard { get; init; }

    public void AddCopies(int numberOfCopies)
    {
        Copies += numberOfCopies;
    }
}
