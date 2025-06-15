using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Commands.Checks;
using Revolt.Net.Commands.TypeBinding;
using System.Diagnostics.CodeAnalysis;

namespace Revolt.Net.Commands.Models
{
    internal sealed class RevoltCommandsState
    {
        public readonly IReadOnlyList<RevoltCommandDescriptor> Commands;

        private readonly IReadOnlyList<Type> TypelessHandlers;
        private readonly Dictionary<Type, RevoltCommandPreCheckDescriptor> PreCheckDescriptors;
        private readonly Dictionary<Type, RevoltCommandTypeParserDescriptor> GenericTypeHandlers;

        public RevoltCommandsState(
            IEnumerable<Type> commandModuleTypes,
            List<Type> typeHandlers,
            Dictionary<Type, Type> genericTypeHandlers,
            Dictionary<Type, Type> preChecks)
        {
            PreCheckDescriptors = preChecks
                .ToDictionary(x => x.Key, x => RevoltCommandPreCheckDescriptor.FromHandler(x.Key, x.Value));

            TypelessHandlers = typeHandlers;

            Commands = [.. commandModuleTypes.SelectMany(RevoltCommandDescriptor.GetCommandsFromModuleType)];

            GenericTypeHandlers = genericTypeHandlers.ToDictionary(x => x.Key, x => RevoltCommandTypeParserDescriptor.FromParser(x.Value));
        }

        public IEnumerable<RevoltCommandDescriptor> GetCommands(string name) =>
            Commands.Where(x => x.Command.Equals(name, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<(RevoltCommandPreCheckDescriptor, ICommandPreCheck)> GetPreCheckDescriptors(RevoltCommandDescriptor command) =>
            command.PreChecks.Select(x => (PreCheckDescriptors[x.CheckType], x.CheckData));

        public bool TryGetGenericTypeParser(Type valueType, IServiceProvider provider, [NotNullWhen(true)] out (object ParserInstance, RevoltCommandTypeParserDescriptor Data) result)
        {
            if (GenericTypeHandlers.TryGetValue(valueType, out var parserData))
            {
                var parser = ActivatorUtilities.GetServiceOrCreateInstance(provider, parserData.ParserType);

                result = (parser, parserData);

                return true;
            }

            result = default;

            return false;
        }

        public IEnumerable<ITypeLessParser> GetTypelessHandlers(IServiceProvider provider)
        {
            foreach (var type in TypelessHandlers)
            {
                yield return (ITypeLessParser)ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
            }
        }
    }
}
