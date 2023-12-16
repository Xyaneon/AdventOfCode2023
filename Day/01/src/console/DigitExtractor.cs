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

    private static Regex firstDigitRegex = new Regex(digitRegex);
    private static Regex lastDigitRegex = new Regex(digitRegex, RegexOptions.RightToLeft);

    public static int GetFirstDigit(string line) =>
        ConvertDigitToInt(GetFirstDigitString(line));

    public static int GetLastDigit(string line) =>
        ConvertDigitToInt(GetLastDigitString(line));

    private static string GetFirstDigitString(string line) =>
        firstDigitRegex.Match(line).Groups[1].ToString();

    private static string GetLastDigitString(string line) =>
        lastDigitRegex.Match(line).Groups[1].ToString();

    private static int ConvertDigitToInt(string digit) =>
        digit.Length == 1 ? int.Parse(digit) : digits[digit];
}
