namespace Revolt.Net.Commands.Parsers.Defaults
{
    internal sealed record DefaultIntTypeParser : ITypeParser<int>
    {
        public Task<TypeParseResult> ParseAsync(TypeParserContext context, CancellationToken cancellationToken)
        {
            var result = TypeParseResult.Failed();

            if (int.TryParse(context.RawArgument, out var value))
            {
                result = TypeParseResult.Success(value);
            }

            return Task.FromResult(result);
        }
    }
}