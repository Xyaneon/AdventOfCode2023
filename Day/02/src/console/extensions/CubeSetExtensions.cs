namespace Extensions;

static class CubeSetExtensions
{
    public static bool IsPossible(this CubeSet set, int red, int green, int blue) =>
        red >= set.RedCubeCount && green >= set.GreenCubeCount && blue >= set.BlueCubeCount;
    
    public static int Power(this CubeSet set) =>
        set.RedCubeCount * set.GreenCubeCount * set.BlueCubeCount;
}
