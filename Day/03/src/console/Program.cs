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
}
