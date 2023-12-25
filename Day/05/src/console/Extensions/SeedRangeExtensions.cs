using Domain;

namespace Extensions;

static class SeedRangeExtensions
{
    public static bool Intersects(this SeedRange seedRange, SeedRange other) =>
        seedRange.StartNumber <= other.EndNumber && seedRange.EndNumber >= other.StartNumber;


    public static IEnumerable<long> GetAllSeeds(this SeedRange seedRange)
    {
        for (long seed = seedRange.StartNumber; seed <= seedRange.EndNumber; seed++)
        {
            yield return seed;
        }
    }
}
