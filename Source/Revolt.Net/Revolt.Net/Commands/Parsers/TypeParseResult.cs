namespace Revolt.Net.Commands.Parsers
{
    public sealed record TypeParseResult(TypeParseStatus Status, object? Value)
    {
        public static TypeParseResult Success(object value) => new TypeParseResult(TypeParseStatus.Success, value);

        public static TypeParseResult Failed() => new TypeParseResult(TypeParseStatus.Failed, null);

    }
}
