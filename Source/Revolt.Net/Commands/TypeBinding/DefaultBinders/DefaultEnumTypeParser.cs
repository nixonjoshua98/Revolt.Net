namespace Revolt.Net.Commands.TypeBinding.DefaultBinders
{
    internal sealed record DefaultEnumTypeParser : ITypeParser
    {
        public Task<bool> CanParseAsync(TypeParserContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Type.IsEnum);
        }

        public Task<TypeParseResult> ParseAsync(TypeParserContext context, CancellationToken cancellationToken)
        {
            var result = TypeParseResult.Failed();

            if (Enum.TryParse(context.Type, context.RawArgument, out var value))
            {
                result = TypeParseResult.Success(value);
            }

            return Task.FromResult(result);
        }
    }
}