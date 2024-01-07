using System.Collections.Immutable;
using Calculations;
using Extensions;

namespace Domain;

public class Hand : IComparable<Hand>
{
    private const int HandSize = 5;

    private HandType? _handType;

    public Hand(string labels)
    {
        if (labels.Length != HandSize)
        {
            throw new ArgumentException($"Hand size expected to be {HandSize}, but got {labels.Length}.");
        }
        Labels = ParseLabels(labels);
    }

    public IReadOnlyList<Label> Labels { get; init; }

    public HandType GetHandType()
    {
        if (_handType is not null)
            return _handType.Value;
        
        _handType = HandTypeDeterminer.DetermineHandType(this);
        return _handType.Value;
    }

    public override string ToString() => string.Join("", Labels.Select(label => label.ToChar()));

    public int CompareTo(Hand? other)
    {
        if (other is null)
            return 1;

        int handTypeComparison = GetHandType().CompareTo(other.GetHandType());

        return handTypeComparison != 0
            ? handTypeComparison
            : CompareLabels(other);
    }

    private static ImmutableList<Label> ParseLabels(string labels) =>
        ImmutableList.CreateRange(labels.Select(chr => chr.ToLabel()));

    private int CompareLabels(Hand other)
    {
        for (int i = 0; i < HandSize; i++)
        {
            int comparison = Labels[i].CompareTo(other.Labels[i]);

            if (comparison != 0)
                return comparison;
        }
        return 0;
    }
}
