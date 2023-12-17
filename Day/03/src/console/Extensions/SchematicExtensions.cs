namespace Extensions;

static class SchematicExtensions
{
    public static IEnumerable<Gear> FindGears(this Schematic schematic) =>
        schematic.RetrieveRowNumbers()
            .SelectMany(rowNumber => schematic.FindGearsInLine(rowNumber));

    public static IEnumerable<int> FindPartNumbers(this Schematic schematic) =>
        schematic.RetrieveRowNumbers()
            .SelectMany(rowNumber => schematic.FindPartNumbersInLine(rowNumber));
    
    public static SchematicLine? RetrieveLineAbove(this Schematic schematic, int rowNumber) =>
        rowNumber == 0 ? null : schematic.Lines.ElementAt(rowNumber - 1);

    public static SchematicLine? RetrieveLineBelow(this Schematic schematic, int rowNumber) =>
        rowNumber == schematic.Lines.Count - 1 ? null : schematic.Lines.ElementAt(rowNumber + 1);

    public static IEnumerable<int> RetrieveRowNumbers(this Schematic schematic) =>
        Enumerable.Range(0, schematic.Lines.Count);


    private static IEnumerable<Gear> FindGearsInLine(this Schematic schematic, int rowNumber)
    {
        IEnumerable<int> possibleGearPositions = schematic.Lines.ElementAt(rowNumber).Symbols
            .Where(symbol => symbol.Value == '*')
            .Select(symbol => symbol.Key);

        foreach (int gearPosition in possibleGearPositions)
        {
            var adjacentNumbers = FindAdjacentNumbers(gearPosition,
                                                      schematic.RetrieveLineAbove(rowNumber),
                                                      schematic.Lines.ElementAt(rowNumber),
                                                      schematic.RetrieveLineBelow(rowNumber))
                .ToList();
            
            if (adjacentNumbers.Count == 2)
            {
                yield return new Gear(rowNumber,
                                      gearPosition,
                                      adjacentNumbers.ElementAt(0).Value,
                                      adjacentNumbers.ElementAt(1).Value);
            }
        }
    }

    private static IEnumerable<SchematicNumber> FindAdjacentNumbers(int gearPosition,
                                                                    SchematicLine? lineAbove,
                                                                    SchematicLine currentLine,
                                                                    SchematicLine? lineBelow) =>
        FindAdjacentNumbersOnAdjacentLine(gearPosition, lineAbove)
            .Concat(FindAdjacentNumbersOnSameLine(gearPosition, currentLine))
            .Concat(FindAdjacentNumbersOnAdjacentLine(gearPosition, lineBelow));

    private static IEnumerable<SchematicNumber> FindAdjacentNumbersOnAdjacentLine(int gearPosition, SchematicLine? line) =>
        line?.Numbers
            .Where(number => gearPosition >= number.FirstDigitPosition - 1 && gearPosition <= number.LastDigitPosition + 1)
            ?? new List<SchematicNumber>(0);

    private static IEnumerable<SchematicNumber> FindAdjacentNumbersOnSameLine(int gearPosition, SchematicLine line) =>
        line.Numbers
            .Where(number => number.FirstDigitPosition == gearPosition + 1 || number.LastDigitPosition == gearPosition - 1);

    private static IEnumerable<int> FindPartNumbersInLine(this Schematic schematic, int rowNumber) =>
        schematic.Lines.ElementAt(rowNumber).Numbers
            .Where(number => IsAPartNumber(number,
                                           schematic.RetrieveLineAbove(rowNumber),
                                           schematic.Lines.ElementAt(rowNumber),
                                           schematic.RetrieveLineBelow(rowNumber)))
            .Select(number => number.Value);

    private static bool IsAPartNumber(SchematicNumber number,
                                      SchematicLine? lineAbove,
                                      SchematicLine currentLine,
                                      SchematicLine? lineBelow) =>
        IsNumberAdjacentToSymbolOnAdjacentLine(number, lineAbove)
            || IsNumberAdjacentToSymbolOnSameLine(number, currentLine)
            || IsNumberAdjacentToSymbolOnAdjacentLine(number, lineBelow);

    private static bool IsNumberAdjacentToSymbolOnAdjacentLine(SchematicNumber number, SchematicLine? line) =>
        line?.RetrieveSymbolPositions()
            .Any(position => position >= number.FirstDigitPosition - 1 && position <= number.LastDigitPosition + 1)
            ?? false;

    private static bool IsNumberAdjacentToSymbolOnSameLine(SchematicNumber number, SchematicLine line) =>
        line.RetrieveSymbolPositions()
            .Any(position => position == number.FirstDigitPosition - 1 || position == number.LastDigitPosition + 1);
}
