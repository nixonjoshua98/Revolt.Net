using Revolt.Net.Core.Exceptions;
using System.Reflection;

namespace Revolt.Net.Commands.TypeBinding
{
    internal sealed record RevoltCommandTypeParserDescriptor(Type ParserType, MethodInfo ParseMethod)
    {
        public static RevoltCommandTypeParserDescriptor FromParser(Type parserType)
        {
            var parseMethod = parserType.GetMethod(nameof(ITypeParser<>.ParseAsync))
                ?? throw new RevoltException("Command type binder is missing required method");

            return new RevoltCommandTypeParserDescriptor(parserType, parseMethod);
        }
    }

}
