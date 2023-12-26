using Domain;

static class RecordsParser
{
    public static IEnumerable<RaceRecord> ParseRecords(IList<string> lines)
    {
        if (lines.Count != 2)
        {
            throw new ArgumentException($"Exactly two lines must be supplied ({lines.Count} provided).", nameof(lines));
        }

        IList<long> times = ParseNumbersLine(lines[0]);
        IList<long> distances = ParseNumbersLine(lines[1]);

        if (times.Count != distances.Count)
        {
            throw new ArgumentException($"{times.Count:N0} times provided, but {distances.Count:N0} distances also provided.", nameof(lines));
        }

        for (int i = 0; i < times.Count; i++)
        {
            yield return new RaceRecord(times[i], distances[i]);
        }
    }

    public static RaceRecord ParseAsSingleRecord(IList<string> lines)
    {
        if (lines.Count != 2)
        {
            throw new ArgumentException($"Exactly two lines must be supplied ({lines.Count} provided).", nameof(lines));
        }

        long time = ParseNumberLine(lines[0]);
        long distance = ParseNumberLine(lines[1]);

        return new RaceRecord(time, distance);
    }

    private static long ParseNumberLine(string line) =>
        long.Parse(line.Split(' ', 2)[1].Replace(" ", ""));

    private static IList<long> ParseNumbersLine(string line) =>
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Skip(1)
            .Select(x => long.Parse(x))
            .ToList();
}
