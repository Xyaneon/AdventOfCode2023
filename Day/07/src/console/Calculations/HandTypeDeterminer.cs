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

    private static Dictionary<char, int> GetCardCounts(string hand)
    {
        var cardCounts = InitializeEmptyCardCounts();
        foreach (char label in hand)
        {
            cardCounts[label] = cardCounts[label] + 1;
        }
        return cardCounts;
    }

    private static Dictionary<char, int> InitializeEmptyCardCounts() =>
        new()
        {
            { '2', 0 },
            { '3', 0 },
            { '4', 0 },
            { '5', 0 },
            { '6', 0 },
            { '7', 0 },
            { '8', 0 },
            { '9', 0 },
            { 'T', 0 },
            { 'J', 0 },
            { 'Q', 0 },
            { 'K', 0 },
            { 'A', 0 },
        };

    private static bool IsFiveOfAKind(Dictionary<char, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 5);

    private static bool IsFourOfAKind(Dictionary<char, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 4);
    
    private static bool IsFullHouse(Dictionary<char, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 3) && cardCounts.Values.Any(value => value == 2);
    
    private static bool IsThreeOfAKind(Dictionary<char, int> cardCounts) =>
        cardCounts.Values.Any(value => value == 3);
    
    private static bool IsTwoPair(Dictionary<char, int> cardCounts) =>
        cardCounts.Values.Where(value => value == 2).Count() == 2;

    private static bool IsOnePair(Dictionary<char, int> cardCounts) =>
        cardCounts.Values.Where(value => value == 2).Count() == 1;
}
