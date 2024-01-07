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
        var cardCounts = new Dictionary<Label, int>();
        foreach (var label in labels)
        {
            cardCounts[label] = cardCounts.GetValueOrDefault(label, 0) + 1;
        }
        return cardCounts;
    }

    private static bool IsFiveOfAKind(Dictionary<Label, int> cardCounts) => HasCardsOfSameKind(cardCounts, 5);

    private static bool IsFourOfAKind(Dictionary<Label, int> cardCounts) => HasCardsOfSameKind(cardCounts, 4);

    private static bool IsFullHouse(Dictionary<Label, int> cardCounts) =>
        CountOfDifferentNonJokerLabels(cardCounts) <= 2;

    private static bool IsThreeOfAKind(Dictionary<Label, int> cardCounts) => HasCardsOfSameKind(cardCounts, 3);

    private static bool IsTwoPair(Dictionary<Label, int> cardCounts) => cardCounts.GetValueOrDefault(Label.Joker, 0) switch
    {
        >= 2 => true,
        1 => CountOfDifferentNonJokerLabels(cardCounts) == 2 || CountOfDifferentNonJokerLabels(cardCounts) == 3,
        <= 0 => GetNonJokerLabels(cardCounts).Where(label => cardCounts[label] >= 2).Count() == 2,
    };

    private static bool IsOnePair(Dictionary<Label, int> cardCounts) =>
        cardCounts.GetValueOrDefault(Label.Joker, 0) == 2
        || GetNonJokerLabels(cardCounts).Any(label => cardCounts[label] + cardCounts.GetValueOrDefault(Label.Joker, 0) == 2);
    
    private static bool HasCardsOfSameKind(Dictionary<Label, int> cardCounts, int count) =>
        cardCounts.GetValueOrDefault(Label.Joker, 0) == count
        || GetNonJokerLabels(cardCounts).Any(label => cardCounts[label] + cardCounts.GetValueOrDefault(Label.Joker, 0) == count);

    private static int CountOfDifferentNonJokerLabels(Dictionary<Label, int> cardCounts) =>
        GetNonJokerLabels(cardCounts).Where(label => cardCounts[label] >= 1).Count();
    
    private static IEnumerable<Label> GetNonJokerLabels(Dictionary<Label, int> cardCounts) =>
        cardCounts.Keys.Where(label => label != Label.Joker);
}
