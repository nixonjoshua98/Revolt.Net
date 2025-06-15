using Revolt.Net.Core.Exceptions;
using System.Reflection;

namespace Revolt.Net.Commands.Parsers
{
    internal sealed record RevoltCommandTypeParserDescriptor(Type ParserType, MethodInfo ParseMethod)
    {
        public static RevoltCommandTypeParserDescriptor FromParser(Type parserType)
        {
            // parserType.GetMethod(nameof(ITypeParser<>.ParseAsync))
            var parseMethod = parserType.GetMethod("ParseAsync")
                ?? throw new RevoltException("Command type binder is missing required method");

            return new RevoltCommandTypeParserDescriptor(parserType, parseMethod);
        }
    }

}
