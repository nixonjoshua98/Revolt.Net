using System.Reflection;

namespace Revolt.Net.Commands.TypeBinding
{
    internal sealed record TypeParserCachedMetadata(Type BinderType, MethodInfo MethodInfo);
}
