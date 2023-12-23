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
                           .Select(mapRange => CreateMapRangeLine(mapRange)))
            .ToList();

    private static IEnumerable<string> EnumerableFromSingleElement(string value) =>
        new string[] { value };
}
