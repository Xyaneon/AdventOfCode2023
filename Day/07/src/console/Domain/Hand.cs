using Calculations;

namespace Domain;

public class Hand
{
    private HandType? _handType;

    public Hand(string labels)
    {
        Labels = labels;
    }

    public string Labels { get; init; }

    public HandType GetHandType()
    {
        if (_handType is not null)
            return _handType.Value;
        
        _handType = HandTypeDeterminer.DetermineHandType(this);
        return _handType.Value;
    }
}
