namespace Extensions;

static class StringExtensions
{
    public static Scratchcard ParseAsScratchcard(this string str)
    {
        string[] labeledParts = str.Split(':', StringSplitOptions.TrimEntries);
        string[] numberParts = labeledParts[1].Split('|', StringSplitOptions.TrimEntries);

        int cardNumber = ParseCardNumber(labeledParts[0]);
        List<int> winningNumbers = ParseNumberList(numberParts[0]);
        List<int> numbersYouHave = ParseNumberList(numberParts[1]);

        return new Scratchcard(cardNumber, winningNumbers, numbersYouHave);
    }

    private static int ParseCardNumber(string labelText) =>
        int.Parse(labelText.Split(' ', StringSplitOptions.TrimEntries).Last());

    private static List<int> ParseNumberList(string numbersText) =>
        numbersText.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(numberText => int.Parse(numberText))
            .ToList();
}
