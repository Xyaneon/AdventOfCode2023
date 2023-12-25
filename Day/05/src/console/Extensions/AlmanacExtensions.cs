using Domain;

namespace Extensions;

static class AlmanacExtensions
{
    private static readonly Dictionary<MapKind, string> mapKindToHeaderDictionary = new Dictionary<MapKind, string>()
    {
        { MapKind.SeedToSoil, "seed-to-soil map:" },
        { MapKind.SoilToFertilizer, "soil-to-fertilizer map:" },
        { MapKind.FertilizerToWater, "fertilizer-to-water map:" },
        { MapKind.WaterToLight, "water-to-light map:" },
        { MapKind.LightToTemperature, "light-to-temperature map:" },
        { MapKind.TemperatureToHumidity, "temperature-to-humidity map:" },
        { MapKind.HumidityToLocation, "humidity-to-location map:" },
    };

    public static IReadOnlyList<string> ToLines(this Almanac almanac) =>
        EnumerableFromSingleElement(CreateSeedLine(almanac))
            .Concat(CreateLinesForAllMaps(almanac))
            .ToList()
            .AsReadOnly();

    public static IEnumerable<long> GetAllSeedsFromRanges(this Almanac almanac)
    {
        ThrowIfSeedListDoesNotContainPairs(almanac);

        for (int i = 0; i < almanac.SeedList.Count - 1; i+=2)
        {
            long startNumber = almanac.SeedList[i];
            long count = almanac.SeedList[i + 1];
            
            for (long seedNumber = startNumber; seedNumber < startNumber + count; seedNumber++)
            {
                yield return seedNumber;
            }
        }
    }

    public static long GetLowestLocationNumberForSeedRange(this Almanac almanac, SeedRange seedRange) =>
        seedRange.GetAllSeeds()
                 .AsParallel()
                 .Select(seed => almanac.GetCorrespondingLocationForSeed(seed))
                 .Min();

    public static long GetLowestLocationNumberForAllSeedRanges(this Almanac almanac) =>
        almanac.GetSeedRanges()
               .AsParallel()
               .Select(range => almanac.GetLowestLocationNumberForSeedRange(range))
               .Min();

    public static IEnumerable<SeedRange> GetSeedRanges(this Almanac almanac)
    {
        ThrowIfSeedListDoesNotContainPairs(almanac);

        for (int i = 0; i < almanac.SeedList.Count - 1; i += 2)
        {
            long startNumber = almanac.SeedList[i];
            long count = almanac.SeedList[i + 1];
            
            yield return new SeedRange(startNumber, count);
        }
    }

    public static long GetCorrespondingLocationForSeed(this Almanac almanac, long seedNumber)
    {
        long soilNumber = almanac.Maps[MapKind.SeedToSoil].GetCorrespondingDestinationNumber(seedNumber);
        long fertilizerNumber = almanac.Maps[MapKind.SoilToFertilizer].GetCorrespondingDestinationNumber(soilNumber);
        long waterNumber = almanac.Maps[MapKind.FertilizerToWater].GetCorrespondingDestinationNumber(fertilizerNumber);
        long lightNumber = almanac.Maps[MapKind.WaterToLight].GetCorrespondingDestinationNumber(waterNumber);
        long temperatureNumber = almanac.Maps[MapKind.LightToTemperature].GetCorrespondingDestinationNumber(lightNumber);
        long humidityNumber = almanac.Maps[MapKind.TemperatureToHumidity].GetCorrespondingDestinationNumber(temperatureNumber);

        return almanac.Maps[MapKind.HumidityToLocation].GetCorrespondingDestinationNumber(humidityNumber);

        // Console.WriteLine($"Seed {seedNumber}, soil {soilNumber}, fertilizer {fertilizerNumber}, water {waterNumber}, light {lightNumber}, temperature {temperatureNumber}, humidity {humidityNumber}, location {locationNumber}");
    }

    public static long GetLowestLocationNumberForInitialSeedNumbers(this Almanac almanac)
    {
        long lowestLocationNumber = long.MaxValue;
        
        foreach (long seedNumber in almanac.SeedList)
        {
            long locationNumber = almanac.GetCorrespondingLocationForSeed(seedNumber);

            if (locationNumber < lowestLocationNumber)
            {
                lowestLocationNumber = locationNumber;
            }
        }

        return lowestLocationNumber;
    }

    private static void ThrowIfSeedListDoesNotContainPairs(Almanac almanac)
    {
        if (almanac.SeedList.Count % 2 != 0)
        {
            throw new InvalidOperationException("Cannot interpret seed list as pairs of values with odd count.");
        }
    }

    private static string CreateSeedLine(Almanac almanac) =>
        "seeds: " + string.Join(' ', almanac.SeedList);

    private static string CreateMapRangeLine(MapRange mapRange) =>
        mapRange.DestinationRangeStart + " " + mapRange.SourceRangeStart + " " + mapRange.RangeLength;

    private static IEnumerable<string> CreateLinesForAllMaps(Almanac almanac) =>
        Enum.GetValues<MapKind>()
            .SelectMany(mapKind => EnumerableFromSingleElement("")
                .Concat(CreateLinesForMap(almanac, mapKind)));

    private static IEnumerable<string> CreateLinesForMap(Almanac almanac, MapKind mapKind) =>
        EnumerableFromSingleElement(mapKindToHeaderDictionary[mapKind])
            .Concat(almanac.Maps[mapKind]
                           .Ranges
                           .Select(mapRange => CreateMapRangeLine(mapRange)));

    private static IEnumerable<string> EnumerableFromSingleElement(string value) =>
        new string[] { value };
}
