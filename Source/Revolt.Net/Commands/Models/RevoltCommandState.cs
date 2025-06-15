using Revolt.Net.Commands.TypeBinding;
using Revolt.Net.Core.Exceptions;
using System.Collections.Concurrent;

namespace Revolt.Net.Commands.Models
{
    internal sealed class RevoltCommandState(List<RegisteredCommand> _registeredCommands)
    {
        public IReadOnlyList<RegisteredCommand> Commands { get; private set; } = _registeredCommands;

        private readonly ConcurrentDictionary<Type, TypeParserCachedMetadata> CachedTypeBinders = [];

        public IEnumerable<RegisteredCommand> GetCommands(string name) =>
            Commands.Where(x => x.Command.Equals(name, StringComparison.OrdinalIgnoreCase));

        public TypeParserCachedMetadata GetTypeBinder(Type valueType)
        {
            return CachedTypeBinders.GetOrAdd(valueType, _ =>
            {
                var binderType = typeof(ITypeParser<>).MakeGenericType(valueType);

                var parseMethod = binderType.GetMethod(nameof(ITypeParser<>.ParseAsync))
                    ?? throw new RevoltException("Command type binder is missed required method");

                return new TypeParserCachedMetadata(binderType, parseMethod);
            });
        }
    }
}
