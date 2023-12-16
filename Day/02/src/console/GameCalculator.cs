class GameCalculator
{
    public static bool IsGamePossible(Game game, int redCubeCount, int greenCubeCount, int blueCubeCount)
    {
        foreach (Round round in game.Rounds)
        {
            if (redCubeCount < round.RedCubeCount
                || greenCubeCount < round.GreenCubeCount
                || blueCubeCount < round.BlueCubeCount)
            {
                return false;
            }
        }

        return true;
    }
}
