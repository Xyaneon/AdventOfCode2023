using System.Collections.Immutable;

using Domain;

using Extensions;

static class AlmanacParser
{
    public static Almanac Parse(IEnumerable<string> lines)
    {
        IEnumerable<int> seedList = ParseSeedList(lines.First());

        var mapToParse = MapKind.SeedToSoil;

        Dictionary<MapKind, List<MapRange>> maps = Enum.GetValues<MapKind>()
            .ToDictionary(key => key, key => new List<MapRange>());

        var headerToMapKindDictionary = new Dictionary<string, MapKind>()
        {
            { "seed-to-soil map:", MapKind.SeedToSoil },
            { "soil-to-fertilizer map:", MapKind.SoilToFertilizer },
            { "fertilizer-to-water map:", MapKind.FertilizerToWater },
            { "water-to-light map:", MapKind.WaterToLight },
            { "light-to-temperature map:", MapKind.LightToTemperature },
            { "temperature-to-humidity map:", MapKind.TemperatureToHumidity },
            { "humidity-to-location map:", MapKind.HumidityToLocation },
        };

        foreach (string line in lines.Skip(1).Select(x => x.Trim()))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            MapKind nextMapKind;
            if (headerToMapKindDictionary.TryGetValue(line, out nextMapKind))
            {
                mapToParse = nextMapKind;
                continue;
            }

            MapRange mapRange = line.ParseToMapRange();
            maps[mapToParse].Add(mapRange);
        }

        return new Almanac(ImmutableList.ToImmutableList(seedList),
                            maps.ToImmutableDictionary(key => key.Key, key => maps[key.Key].ToImmutableList()));
    }

    private static IEnumerable<int> ParseSeedList(string line)
    {
        string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (tokens[0] != "seeds:")
        {
            throw new ArgumentException("Seed list line does not start with expected format.", nameof(line));
        }

        try
        {
            return tokens.AsEnumerable()
                         .Skip(1)
                         .Select(token => int.Parse(token));
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could not parse a number in the seed list line.", nameof(line), ex);
        }
    }
}
