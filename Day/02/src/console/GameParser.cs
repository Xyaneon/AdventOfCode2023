class GameParser
{
    public static List<Game> ParseGames(IEnumerable<string> lines) =>
        lines.Select(line => ParseGame(line)).ToList();

    private static Game ParseGame(string line)
    {
        (int id, string remainder) = ExtractGameId(line);
        List<CubeSet> rounds = ParseRounds(remainder);

        return new Game(id, rounds);
    }

    private static List<CubeSet> ParseRounds(string lineRemainder) =>
        lineRemainder.Split(';', StringSplitOptions.TrimEntries)
            .Select(x => ParseRound(x))
            .ToList();

    private static CubeSet ParseRound(string str)
    {
        int redCubeCount = 0;
        int greenCubeCount = 0;
        int blueCubeCount = 0;

        string[] parts = str.Split(',', StringSplitOptions.TrimEntries);

        foreach (string part in parts)
        {
            if (part.EndsWith("red"))
            {
                redCubeCount += ExtractCubeCountFromRoundPart(part);
            }
            else if (part.EndsWith("green"))
            {
                greenCubeCount += ExtractCubeCountFromRoundPart(part);
            }
            else if (part.EndsWith("blue"))
            {
                blueCubeCount += ExtractCubeCountFromRoundPart(part);
            }
        }

        return new CubeSet(redCubeCount, greenCubeCount, blueCubeCount);
    }

    private static (int id, string remainder) ExtractGameId(string line)
    {
        string[] portions = line.Split(':', StringSplitOptions.TrimEntries);
        int id = int.Parse(portions[0].Split(' ', StringSplitOptions.TrimEntries).Last());

        return (id, portions[1]);
    }

    private static int ExtractCubeCountFromRoundPart(string roundPart) =>
        int.Parse(roundPart.Split(' ', StringSplitOptions.TrimEntries).First());
}
