namespace Revolt.Net.Commands.Parsers
{
    public sealed record TypeParserContext(
        CommandContext CommandContext,
        Type TargetType,
        string RawArgument
    );
}
