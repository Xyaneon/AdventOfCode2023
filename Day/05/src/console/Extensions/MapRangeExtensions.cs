using Domain;

namespace Extensions;

static class MapRangeExtensions
{
    public static bool IsSourceNumberInRange(this MapRange mapRange, long sourceNumber) =>
        sourceNumber >= mapRange.SourceRangeStart && sourceNumber <= mapRange.SourceRangeEnd;

    public static long GetCorrespondingDestinationNumber(this MapRange mapRange, long sourceNumber) =>
        mapRange.IsSourceNumberInRange(sourceNumber)
            ? sourceNumber - mapRange.SourceRangeStart + mapRange.DestinationRangeStart
            : sourceNumber;
    
    public static bool SourceRangeIntersects(this MapRange mapRange, MapRange other) =>
        mapRange.SourceRangeStart <= other.SourceRangeEnd && mapRange.SourceRangeEnd >= other.SourceRangeStart;

    public static bool DestinationRangeIntersects(this MapRange mapRange, MapRange other) =>
        mapRange.DestinationRangeStart <= other.DestinationRangeEnd && mapRange.DestinationRangeEnd >= other.DestinationRangeStart;
}
