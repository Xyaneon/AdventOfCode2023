using Domain;

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

IEnumerable<RaceRecord> raceRecords = RecordsParser.ParseRecords(lines);

int product = raceRecords.Select(raceRecord => Calculator.ComputeNumberOfWaysToBeatRecord(raceRecord))
                         .Aggregate(1, (product, nextNumber) => product * nextNumber);

foreach (var raceRecord in raceRecords)
{
    Console.WriteLine($"Record for {raceRecord.Time} milliseconds: {raceRecord.Distance} millimeters");
    Console.WriteLine($"  Number of ways to beat record: {Calculator.ComputeNumberOfWaysToBeatRecord(raceRecord)}");
}

Console.WriteLine($"Product of all ways to beat records: {product}");

return 0;
