namespace Domain;

record SeedRange(long StartNumber, long Count)
{
    public long EndNumber { get => StartNumber + Count - 1; }
}
