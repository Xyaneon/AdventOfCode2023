using Domain;

namespace Calculations;

public class SinglePathNavigator
{
    public SinglePathNavigator(string startNode)
    {
        CurrentNode = startNode;
    }

    public string CurrentNode { get; private set; }

    public bool OnAnEndingNode { get => CurrentNode.EndsWith('Z'); }

    public void Navigate(Network network, Direction direction)
    {
        CurrentNode = direction == Direction.Left
            ? network.GetLeftNode(CurrentNode)
            : network.GetRightNode(CurrentNode);
    }
}
