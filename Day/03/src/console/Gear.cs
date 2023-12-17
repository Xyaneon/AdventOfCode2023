record Gear(int RowNumber, int Position, int PartNumber1, int PartNumber2)
{
    public int Ratio { get => PartNumber1 * PartNumber2; }
}
