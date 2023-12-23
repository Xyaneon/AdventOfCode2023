using Domain;

namespace Extensions;

static class MapRangeExtensions
{
    public static bool IsSourceNumberInRange(this MapRange mapRange, long sourceNumber) =>
        sourceNumber >= mapRange.SourceRangeStart && sourceNumber < mapRange.SourceRangeStart + mapRange.RangeLength;

    public static long GetCorrespondingDestinationNumber(this MapRange mapRange, long sourceNumber) =>
        mapRange.IsSourceNumberInRange(sourceNumber)
            ? sourceNumber - mapRange.SourceRangeStart + mapRange.DestinationRangeStart
            : sourceNumber;
}
