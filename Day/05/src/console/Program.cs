using Domain;

using Extensions;

if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day5.exe input-file");
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

Almanac almanac = AlmanacParser.Parse(lines);


OutputWriter.PrintAlmanac(almanac);

// TODO

return 0;


static class OutputWriter
{
    public static void PrintAlmanac(Almanac almanac)
    {
        foreach (string line in almanac.ToLines())
        {
            Console.WriteLine(line);
        }
    }
}
