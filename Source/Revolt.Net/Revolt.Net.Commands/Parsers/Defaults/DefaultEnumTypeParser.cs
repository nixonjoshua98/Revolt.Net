namespace Revolt.Net.Commands.Parsers.Defaults
{
    internal sealed record DefaultEnumTypeParser : ITypeLessParser
    {
        public Task<bool> CanParseAsync(TypeParserContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.TargetType.IsEnum);
        }

        public Task<TypeParseResult> ParseAsync(TypeParserContext context, CancellationToken cancellationToken)
        {
            var result = TypeParseResult.Failed();

            if (Enum.TryParse(context.TargetType, context.RawArgument, out var value))
            {
                result = TypeParseResult.Success(value);
            }

            return Task.FromResult(result);
        }
    }
}