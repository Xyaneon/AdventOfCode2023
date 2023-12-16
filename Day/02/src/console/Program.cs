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

foreach (Game game in games)
{
    bool isPossible = GameCalculator.IsGamePossible(game, redCount, greenCount, blueCount);
    Console.Write($"Game {game.Id}: ");
    Console.WriteLine(isPossible ? "POSSIBLE" : "NOT POSSIBLE");

    foreach (Round round in game.Rounds)
    {
        Console.WriteLine($"{round.RedCubeCount} red, {round.GreenCubeCount} green, {round.BlueCubeCount} blue");
    }
}

// TODO

return 0;
