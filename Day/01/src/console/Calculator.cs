class Calculator
{
    public static int CalculateCalibrationValue(string line) =>
        DigitExtractor.GetFirstDigit(line) * 10 + DigitExtractor.GetLastDigit(line);
}
