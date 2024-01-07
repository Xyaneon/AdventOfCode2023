using Domain;

namespace Extensions;

static class StringExtensions {
    public static PuzzleInputLine ParseAsPuzzleInputLine(this string str)
    {
        string[] inputParts = str.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (inputParts.Length != 2)
        {
            throw new ArgumentException($"Expected 2 parts, but got {inputParts.Length}.", nameof(str));
        }

        return new PuzzleInputLine(new Hand(inputParts[0]), int.Parse(inputParts[1]));
    }
}
