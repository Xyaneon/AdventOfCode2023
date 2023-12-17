namespace Extensions;

static class RoundExtensions
{
    public static bool IsPossible(this Round round, int red, int green, int blue) =>
        red >= round.RedCubeCount && green >= round.GreenCubeCount && blue >= round.BlueCubeCount;
}
