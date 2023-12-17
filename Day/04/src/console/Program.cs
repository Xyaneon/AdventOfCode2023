using System.Reflection.Metadata.Ecma335;

using Extensions;

if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day4.exe input-file");
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

IEnumerable<Scratchcard> scratchcards = lines.Select(line => line.ParseAsScratchcard());

OutputWriter.PrintScratchcards(scratchcards);

var game = new PartTwoGame(scratchcards);
game.Play();

Console.WriteLine();
OutputWriter.PrintPartTwoGame(game);

return 0;


static class OutputWriter
{
    public static void PrintScratchcards(IEnumerable<Scratchcard> scratchcards)
    {
        foreach (Scratchcard scratchcard in scratchcards)
        {
            PrintScratchcard(scratchcard);
        }

        Console.WriteLine();
        Console.WriteLine($"Total point value: {scratchcards.Select(card => card.CalculatePoints()).Sum()}");
    }

    public static void PrintScratchcard(Scratchcard scratchcard)
    {
        Console.WriteLine(Format(scratchcard));
        Console.WriteLine($"\t Matching numbers: {string.Join(' ', scratchcard.DetermineMatchingNumbers())}");
        Console.WriteLine($"\t Points          : {string.Join(' ', scratchcard.CalculatePoints())}");
    }

    public static void PrintPartTwoGame(PartTwoGame game)
    {
        foreach (CopyableScratchcard scratchcard in game.ScratchcardCopies)
        {
            Console.WriteLine(Format(scratchcard));
        }

        Console.WriteLine();
        Console.WriteLine($"Total number of scratchcards: {game.CountTotalNumberOfScratchcards()}");
    }

    private static string Format(CopyableScratchcard scratchcard) =>
        string.Format("⨯{0} {1}", scratchcard.Copies, Format(scratchcard.Scratchcard));

    private static string Format(Scratchcard scratchcard) =>
        string.Format("Card {0}: {1} | {2}",
                      scratchcard.CardNumber,
                      string.Join(' ', scratchcard.WinningNumbers),
                      string.Join(' ', scratchcard.NumbersYouHave));
}
