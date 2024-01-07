using Domain;

namespace Calculations;

static class HandTypeDeterminer
{
    public static HandType DetermineHandType(Hand hand)
    {
        var cardCounts = GetCardCounts(hand.Labels);

        if (IsFiveOfAKind(cardCounts))
        {
            return HandType.FiveOfAKind;
        }

        if (IsFourOfAKind(cardCounts))
        {
            return HandType.FourOfAKind;
        }

        if (IsFullHouse(cardCounts))
        {
            return HandType.FullHouse;
        }

        if (IsThreeOfAKind(cardCounts))
        {
            return HandType.ThreeOfAKind;
        }

        if (IsTwoPair(cardCounts))
        {
            return HandType.TwoPair;
        }

        if (IsOnePair(cardCounts))
        {
            return HandType.OnePair;
        }

        return HandType.HighCard;
    }

    private static Dictionary<Label, int> GetCardCounts(IEnumerable<Label> labels)
    {
        var cardCounts = InitializeEmptyCardCounts();
        foreach (var label in labels)
        {
            cardCounts[label] = cardCounts[label] + 1;
        }
        return cardCounts;
    }

    private static Dictionary<Label, int> InitializeEmptyCardCounts() =>
        new()
        {
            { Label.Two, 0 },
            { Label.Three, 0 },
            { Label.Four, 0 },
            { Label.Five, 0 },
            { Label.Six, 0 },
            { Label.Seven, 0 },
            { Label.Eight, 0 },
            { Label.Nine, 0 },
            { Label.Ten, 0 },
            { Label.Jack, 0 },
            { Label.Queen, 0 },
            { Label.King, 0 },
            { Label.Ace, 0 },
        };

    private static bool IsFiveOfAKind(Dictionary<Label, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 5);

    private static bool IsFourOfAKind(Dictionary<Label, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 4);
    
    private static bool IsFullHouse(Dictionary<Label, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 3) && cardCounts.Values.Any(value => value == 2);
    
    private static bool IsThreeOfAKind(Dictionary<Label, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 3);
    
    private static bool IsTwoPair(Dictionary<Label, int> cardCounts) =>
        cardCounts.Values.Where(value => value == 2).Count() == 2;

    private static bool IsOnePair(Dictionary<Label, int> cardCounts) =>
        cardCounts.Values.Where(value => value == 2).Count() == 1;
}
