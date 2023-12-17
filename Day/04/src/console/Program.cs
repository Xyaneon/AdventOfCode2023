﻿using Extensions;

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

// TODO

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
        Console.WriteLine("Card {0}: {1} | {2}",
                          scratchcard.CardNumber,
                          string.Join(' ', scratchcard.WinningNumbers),
                          string.Join(' ', scratchcard.NumbersYouHave));
        Console.WriteLine($"\t Matching numbers: {string.Join(' ', scratchcard.DetermineMatchingNumbers())}");
        Console.WriteLine($"\t Points          : {string.Join(' ', scratchcard.CalculatePoints())}");
    }
}
