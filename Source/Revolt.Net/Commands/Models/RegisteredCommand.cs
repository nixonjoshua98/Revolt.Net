using Revolt.Net.Commands.Attributes;
using System.Reflection;

namespace Revolt.Net.Commands.Models
{
    internal sealed record RegisteredCommand(string Command, Type ModuleType, MethodInfo CommandMethod)
    {
        public static IEnumerable<RegisteredCommand> GetCommandsFromModuleType(Type commandModuleType)
        {
            var methods = commandModuleType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var commandAttr = method.GetCustomAttribute<CommandAttribute>();

                if (commandAttr != null)
                {
                    yield return new RegisteredCommand(commandAttr.Name, commandModuleType, method);
                }
            }
        }
    }
}
