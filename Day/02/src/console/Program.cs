if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day2.exe input-file");
    return 1;
}

string[] lines;

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
    Console.WriteLine($"Game {game.Id}:");
    foreach (Round round in game.Rounds)
    {
        Console.WriteLine($"{round.RedCubeCount} red, {round.GreenCubeCount} green, {round.BlueCubeCount} blue");
    }
}

// TODO

return 0;
