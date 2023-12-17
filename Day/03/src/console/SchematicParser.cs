static class SchematicParser
{
    public static Schematic Parse(IEnumerable<string> rawTextLines)
    {
        List<SchematicLine> parsedLines = rawTextLines
            .Select(line => ParseLine(line))
            .ToList();
        
        return new Schematic(parsedLines);
    }

    private static SchematicLine ParseLine(string rawTextLine)
    {
        var numbers = new List<SchematicNumber>();
        var symbols = new Dictionary<int, char>();
        string currentNumber = "";

        for (int i = 0; i < rawTextLine.Length; i++)
        {
            char currentChar = rawTextLine.ElementAt(i);

            if (currentChar == '.')
            {
                if (currentNumber.Length > 0)
                {
                    numbers.Add(CreateSchematicNumber(i, currentNumber));
                    currentNumber = "";
                }
                continue;
            }
            
            if (char.IsDigit(currentChar))
            {
                currentNumber += currentChar;
            }
            else
            {
                if (currentNumber.Length > 0)
                {
                    numbers.Add(CreateSchematicNumber(i, currentNumber));
                    currentNumber = "";
                }

                symbols.Add(i, currentChar);
            }
        }

        if (currentNumber.Length > 0)
        {
            numbers.Add(CreateSchematicNumber(rawTextLine.Length, currentNumber));
        }

        return new SchematicLine(rawTextLine, numbers, symbols);
    }

    private static SchematicNumber CreateSchematicNumber(int currentPosition, string numberText)
    {
        int firstDigitPosition = currentPosition - numberText.Length;
        int lastDigitPosition = currentPosition - 1;
        int value = int.Parse(numberText);

        return new SchematicNumber(value, firstDigitPosition, lastDigitPosition);
    }
}
