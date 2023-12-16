using System.Text.RegularExpressions;

class DigitExtractor
{
    private static readonly Dictionary<string, int> digits = new Dictionary<string, int>
    {
        {"zero", 0},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
    };

    private const string digitRegex = "([0-9]|zero|one|two|three|four|five|six|seven|eight|nine)";

    private static readonly Regex firstDigitRegex = new Regex(digitRegex);
    private static readonly Regex lastDigitRegex = new Regex(digitRegex, RegexOptions.RightToLeft);

    public static int GetFirstDigit(string line) =>
        ConvertDigitToInt(GetDigitString(line, true));

    public static int GetLastDigit(string line) =>
        ConvertDigitToInt(GetDigitString(line, false));

    private static string GetDigitString(string line, bool first)
    {
        Regex regex = first ? firstDigitRegex : lastDigitRegex;
        return regex.Match(line).Groups[1].ToString();
    }

    private static int ConvertDigitToInt(string digit) =>
        digit.Length == 1 ? int.Parse(digit) : digits[digit];
}
