namespace Extensions;

static class GameExtensions
{
    public static bool IsPossible(this Game game, int red, int green, int blue) =>
        game.Rounds.All(round => round.IsPossible(red, green, blue));

    public static CubeSet FewestPossibleCubes(this Game game)
    {
        int minimumRed = game.Rounds.Select(round => round.RedCubeCount).Max();
        int minimumGreen = game.Rounds.Select(round => round.GreenCubeCount).Max();
        int minimumBlue = game.Rounds.Select(round => round.BlueCubeCount).Max();

        return new CubeSet(minimumRed, minimumGreen, minimumBlue);
    }
}
