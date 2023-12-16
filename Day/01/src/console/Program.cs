if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day1.exe input-file");
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

int calibrationValue = 0;

foreach (var line in lines)
{
    int lineValue = Calculator.CalculateCalibrationValue(line);
    Console.WriteLine($"{line} (value: {lineValue})");
    calibrationValue += lineValue;
}

Console.WriteLine($"Total value: {calibrationValue}");

return 0;
