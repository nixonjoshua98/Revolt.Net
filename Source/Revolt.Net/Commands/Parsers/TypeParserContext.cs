using System.Reflection;

namespace Revolt.Net.Commands.TypeBinding
{
    public sealed record TypeParserContext(
        CommandContext CommandContext,
        Type Type,
        string RawArgument
    );
}
