namespace Domain;

record MapRange(long DestinationRangeStart, long SourceRangeStart, long RangeLength)
{
    public long DestinationRangeEnd { get => DestinationRangeStart + RangeLength - 1; }

    public long SourceRangeEnd { get => SourceRangeStart + RangeLength - 1; }
}
