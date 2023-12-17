namespace Extensions;

static class SchematicLineExtensions
{
    public static IEnumerable<int> RetrieveSymbolPositions(this SchematicLine line) =>
        line.Symbols.AsEnumerable().Select(pair => pair.Key);
}
