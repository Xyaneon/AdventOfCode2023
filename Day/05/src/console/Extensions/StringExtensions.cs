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

        try
        {
            int destinationRangeStart = int.Parse(numbers[0]);
            int sourceRangeStart = int.Parse(numbers[1]);
            int rangeLength = int.Parse(numbers[2]);

            return new MapRange(destinationRangeStart, sourceRangeStart, rangeLength);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Could not parse a number in the MapRange.", ex);
        }
    }
}
