using Domain;

namespace Extensions;

public static class LabelExtensions
{
    public static char ToChar(this Label label) => label switch
    {
        Label.Two => '2',
        Label.Three => '3',
        Label.Four => '4',
        Label.Five => '5',
        Label.Six => '6',
        Label.Seven => '7',
        Label.Eight => '8',
        Label.Nine => '9',
        Label.Ten => 'T',
        Label.Joker => 'J',
        Label.Queen => 'Q',
        Label.King => 'K',
        Label.Ace => 'A',
        _ => throw new ArgumentOutOfRangeException(nameof(label), $"{label} is not a supported value."),
    };
}
