namespace Extensions;

static class GameExtensions
{
    public static bool IsPossible(this Game game, int red, int green, int blue) =>
        game.Rounds.All(round => round.IsPossible(red, green, blue));
}
