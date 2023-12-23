using Domain;

namespace Extensions;

static class StringExtensions
{
    public static MapRange ParseToMapRange(this string str)
    {
        string[] numbers = str.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (numbers.Length != 3)
        {
            throw new InvalidOperationException($"The line to parse as a MapRange must have three parts (found {numbers.Length}).");
        }

        long destinationRangeStart;
        long sourceRangeStart;
        long rangeLength;

        try
        {
            destinationRangeStart = long.Parse(numbers[0]);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Could not parse the destination range number \"{numbers[0]}\" in the MapRange.", ex);
        }

        try
        {
            sourceRangeStart = long.Parse(numbers[1]);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Could not parse the source range number \"{numbers[1]}\" in the MapRange.", ex);
        }

        try
        {
            rangeLength = long.Parse(numbers[2]);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Could not parse the range length \"{numbers[2]}\" in the MapRange.", ex);
        }

        return new MapRange(destinationRangeStart, sourceRangeStart, rangeLength);
    }
}
