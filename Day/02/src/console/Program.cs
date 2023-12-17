using Extensions;

if (args.Length != 4)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 4, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day2.exe input-file red green blue");
    return 1;
}

string[] lines;

int redCount = int.Parse(args[1]);
int greenCount = int.Parse(args[2]);
int blueCount = int.Parse(args[3]);

try
{
    lines = File.ReadAllLines(args[0]);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Failed to read input file: {ex.Message}");
    return 2;
}

List<Game> games = GameParser.ParseGames(lines);
var idsOfPossibleGames = games
    .FindAll(game => game.IsPossible(redCount, greenCount, blueCount))
    .Select(game => game.Id);

OutputWriter.WriteAllGames(games, redCount, greenCount, blueCount);
OutputWriter.WriteResults(idsOfPossibleGames);

return 0;

static class OutputWriter
{
    public static void WriteAllGames(List<Game> games, int red, int green, int blue)
    {
        foreach (Game game in games)
        {
            Console.WriteLine("## Game {0:N0}: {1} POSSIBLE", game.Id, game.IsPossible(red, green, blue) ? "" : "NOT");

            Console.WriteLine();
            Console.WriteLine("This game had **{0:N0} rounds**.", game.Rounds.Count);
            Console.WriteLine();

            WriteAllRounds(game, red, green, blue);

            Console.WriteLine();
        }
    }

    public static void WriteAllRounds(Game game, int red, int green, int blue)
    {
        Console.WriteLine("| Red  | Green | Blue |");
        Console.WriteLine("| ---- | ----- | ---- |");
        foreach (Round round in game.Rounds)
        {
            Console.WriteLine("| {0,2:N0} {1} | {2,2:N0} {3}  | {4,2:N0} {5} |",
                          round.RedCubeCount,
                          red < round.RedCubeCount ? "⨯" : "✔",
                          round.GreenCubeCount,
                          green < round.GreenCubeCount ? "⨯" : "✔",
                          round.BlueCubeCount,
                          blue < round.BlueCubeCount ? "⨯" : "✔");
        }
    }

    public static void WriteResults(IEnumerable<int> idsOfPossibleGames)
    {
        Console.WriteLine("## Results");
        Console.WriteLine();
        Console.WriteLine($"IDs of all possible games: {string.Join('+', idsOfPossibleGames)}");
        Console.WriteLine($"Sum of IDs of possible games: **{idsOfPossibleGames.Sum()}**");
    }
}
