﻿using Extensions;

if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day3.exe input-file");
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

Schematic schematic = SchematicParser.Parse(lines);
OutputWriter.PrintParsedSchematic(schematic);

IEnumerable<int> partNumbers = schematic.FindPartNumbers();
OutputWriter.PrintPartNumbers(partNumbers);

IEnumerable<Gear> gears = schematic.FindGears();
OutputWriter.PrintGears(gears);

return 0;


static class OutputWriter
{
    public static void PrintParsedSchematic(Schematic schematic)
    {
        foreach (SchematicLine line in schematic.Lines)
        {
            Console.WriteLine(line.RawText);
            Console.WriteLine("\tNumbers: " + string.Join(", ", line.Numbers));
            Console.WriteLine("\tSymbols: " + string.Join(", ", line.Symbols));
        }
    }

    public static void PrintPartNumbers(IEnumerable<int> partNumbers)
    {
        int partNumberSum = partNumbers.Sum();
        Console.WriteLine("Part numbers: " + string.Join(", ", partNumbers));
        Console.WriteLine($"Part number sum: {partNumberSum}");
    }

    public static void PrintGears(IEnumerable<Gear> gears)
    {
        int gearRatioSum = gears.Select(gear => gear.Ratio).Sum();
        Console.WriteLine("Gear ratios:\n\t" + string.Join("\n\t", gears));
        Console.WriteLine($"Gear Ratio sum: {gearRatioSum}");
    }
}
