namespace Revolt.Net.Commands.TypeBinding
{
    internal interface ITypeLessParser
    {
        Task<bool> CanParseAsync(TypeParserContext context, CancellationToken cancellationToken);

        Task<TypeParseResult> ParseAsync(TypeParserContext context, CancellationToken cancellationToken);
    }

    public interface ITypeParser<T>
    {
        Task<TypeParseResult> ParseAsync(TypeParserContext context, CancellationToken cancellationToken);
    }
}
