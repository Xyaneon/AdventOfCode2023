using Domain;

namespace Extensions;

static class MapExtensions
{
    public static long GetCorrespondingDestinationNumber(this Map map, long sourceNumber)
    {
        foreach (MapRange mapRange in map.Ranges)
        {
            if (mapRange.IsSourceNumberInRange(sourceNumber))
            {
                return mapRange.GetCorrespondingDestinationNumber(sourceNumber);
            }
        }
        
        return sourceNumber;
    }
}
