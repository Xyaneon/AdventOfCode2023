namespace Extensions;

static class SchematicExtensions
{
    public static IEnumerable<int> FindPartNumbers(this Schematic schematic) =>
        Enumerable.Range(0, schematic.Lines.Count)
            .SelectMany(rowNumber => schematic.FindPartNumbersInLine(rowNumber));

    private static IEnumerable<int> FindPartNumbersInLine(this Schematic schematic, int rowNumber) =>
        schematic.Lines.ElementAt(rowNumber).Numbers
            .Where(number => IsAPartNumber(number,
                                           schematic.RetrieveLineAbove(rowNumber),
                                           schematic.Lines.ElementAt(rowNumber),
                                           schematic.RetrieveLineBelow(rowNumber)))
            .Select(number => number.Value);

    private static bool IsAPartNumber(SchematicNumber number, SchematicLine? lineAbove, SchematicLine currentLine, SchematicLine? lineBelow) =>
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

    private static SchematicLine? RetrieveLineAbove(this Schematic schematic, int rowNumber) =>
        rowNumber == 0 ? null : schematic.Lines.ElementAt(rowNumber - 1);

    private static SchematicLine? RetrieveLineBelow(this Schematic schematic, int rowNumber) =>
        rowNumber == schematic.Lines.Count - 1 ? null : schematic.Lines.ElementAt(rowNumber + 1);
}
