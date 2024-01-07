using Domain;

namespace Extensions;

public static class CharExtensions
{
    private static readonly Dictionary<char, Label> CharToLabelDictionary = new()
    {
        { '2', Label.Two },
        { '3', Label.Three },
        { '4', Label.Four },
        { '5', Label.Five },
        { '6', Label.Six },
        { '7', Label.Seven },
        { '8', Label.Eight },
        { '9', Label.Nine },
        { 'T', Label.Ten },
        { 'J', Label.Joker },
        { 'Q', Label.Queen },
        { 'K', Label.King },
        { 'A', Label.Ace },
    };

    public static Label ToLabel(this char chr) => CharToLabelDictionary[chr];
}
