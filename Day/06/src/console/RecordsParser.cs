using Domain;

static class RecordsParser
{
    public static IEnumerable<RaceRecord> ParseRecords(IList<string> lines)
    {
        if (lines.Count != 2)
        {
            throw new ArgumentException($"Exactly two lines must be supplied ({lines.Count} provided).", nameof(lines));
        }

        IList<int> times = ParseNumbersLine(lines[0]);
        IList<int> distances = ParseNumbersLine(lines[1]);

        if (times.Count != distances.Count)
        {
            throw new ArgumentException($"{times.Count:N0} times provided, but {distances.Count:N0} distances also provided.", nameof(lines));
        }

        for (int i = 0; i < times.Count; i++)
        {
            yield return new RaceRecord(times[i], distances[i]);
        }
    }

    private static IList<int> ParseNumbersLine(string line) =>
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Skip(1)
            .Select(x => int.Parse(x))
            .ToList();
}
