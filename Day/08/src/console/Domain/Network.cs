using System.Collections.Immutable;

namespace Domain;

public class Network
{
    public Network(IEnumerable<NetworkLine> edges)
    {
        Edges = edges.ToImmutableDictionary(line => line.SourceNode, line => (line.LeftNode, line.RightNode));
    }

    public ImmutableDictionary<string, (string, string)> Edges { get; init; }

    public string GetLeftNode(string sourceNode) => Edges[sourceNode].Item1;

    public string GetRightNode(string sourceNode) => Edges[sourceNode].Item2;

    public IEnumerable<string> GetPartTwoStartNodes() =>
        Edges.Keys.Where(sourceNode => sourceNode.EndsWith('A'));

    public override string ToString()
    {
        IEnumerable<string> edgeStrings = Edges.Keys.Select(sourceNode => GetEdgeAsString(sourceNode))
                                                    .OrderBy(x => x);
        return string.Join(Environment.NewLine, edgeStrings);
    }

    private string GetEdgeAsString(string sourceNode) =>
        $"{sourceNode} = ({GetLeftNode(sourceNode)}, {GetRightNode(sourceNode)})";
}
