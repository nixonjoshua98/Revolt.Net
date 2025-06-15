using Revolt.Net.Commands.Abstractions;
using System.Reflection;

namespace Revolt.Net.Commands.Models
{
    internal sealed class RevoltCommandStateBuilder
    {
        private readonly HashSet<Type> RegisteredModuleTypes = [];

        public void AddModule<T>() where T : CommandModule
        {
            AddModule(typeof(T));
        }

        public void AddModule(Type moduleType)
        {
            RegisteredModuleTypes.Add(moduleType);
        }

        public void AddModulesFromAssembly(Assembly assembly)
        {
            var commandModuleType = typeof(CommandModule);

            var modules = assembly.DefinedTypes
                .Where(t => t.IsAssignableTo(commandModuleType));

            foreach (var module in modules)
            {
                AddModule(module);
            }
        }

        public RevoltCommandState Build()
        {
            var registeredCommands = RegisteredModuleTypes
                .SelectMany(RegisteredCommand.GetCommandsFromModuleType)
                .ToList();

            return new RevoltCommandState(registeredCommands);
        }
    }
}
