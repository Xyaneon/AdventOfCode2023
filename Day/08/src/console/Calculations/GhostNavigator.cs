using Domain;

namespace Calculations;

public static class GhostNavigator
{
    public static long GetStepsToNavigate(PuzzleInput puzzleInput)
    {
        var directions = puzzleInput.Directions;
        var network = puzzleInput.Network;

        List<SinglePathNavigator> navigators = network.GetPartTwoStartNodes()
                                                      .Select(node => new SinglePathNavigator(node))
                                                      .ToList();
        int pathsCount = navigators.Count;
        long steps = 0;
        int directionIndex = 0;
        Direction nextDirection;

        while (!navigators.All(navigator => navigator.OnAnEndingNode))
        {
            nextDirection = directions[directionIndex];

            // Console.WriteLine($"Step {steps + 1}, going {nextDirection}:");

            for (int i = 0; i < pathsCount; i++)
            {
                // Console.Write($"  Path {i + 1}: {navigators[i].CurrentNode} -> ");
                navigators[i].Navigate(network, nextDirection);
                // Console.WriteLine($"{navigators[i].CurrentNode}" + (navigators[i].OnAnEndingNode ? " (END NODE)" : ""));
            }

            steps++;
            directionIndex++;

            if (directionIndex >= directions.Count)
                directionIndex = 0;
        }

        return steps;
    }
}
