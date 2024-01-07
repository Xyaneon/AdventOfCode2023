using Domain;

namespace Calculations;

public static class GhostNavigator
{
    public static int GetStepsToNavigate(PuzzleInput puzzleInput)
    {
        var directions = puzzleInput.Directions;
        var network = puzzleInput.Network;

        List<string> currentNodes = network.GetPartTwoStartNodes().ToList();
        int pathsCount = currentNodes.Count;
        int steps = 0;
        int directionIndex = 0;
        Direction nextDirection;

        while (!AllAreEndNodes(currentNodes))
        {
            nextDirection = directions[directionIndex];

            // Console.WriteLine($"Step {steps + 1}, going {nextDirection}:");

            for (int i = 0; i < pathsCount; i++)
            {
                // Console.Write($"  Path {i + 1}: {currentNodes[i]} -> ");
                currentNodes[i] = nextDirection == Direction.Left
                    ? network.GetLeftNode(currentNodes[i])
                    : network.GetRightNode(currentNodes[i]);
                // Console.WriteLine($"{currentNodes[i]}" + (currentNodes[i].EndsWith('Z') ? " (FINISHED)" : ""));
            }

            steps++;
            directionIndex++;

            if (directionIndex >= directions.Count)
                directionIndex = 0;
        }

        return steps;
    }

    private static bool AllAreEndNodes(IEnumerable<string> nodes) =>
        nodes.All(node => node.EndsWith('Z'));
}
