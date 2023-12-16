class DigitExtractor
{
    public static int GetFirstDigit(string line) =>
        ConvertDigitToInt(line.Where(x => char.IsDigit(x)).First());

    public static int GetLastDigit(string line) =>
        ConvertDigitToInt(line.Where(x => char.IsDigit(x)).Last());

    private static int ConvertDigitToInt(char digit) =>
        int.Parse(digit.ToString());
}
