namespace Extensions;

static class CubeSetExtensions
{
    public static bool IsPossible(this CubeSet set, int red, int green, int blue) =>
        red >= set.RedCubeCount && green >= set.GreenCubeCount && blue >= set.BlueCubeCount;
}
