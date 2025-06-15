using Revolt.Net.Commands.Attributes;
using System.Reflection;

namespace Revolt.Net.Commands.Models
{
    internal sealed record RegisteredCommand(
        string Command,
        int Priority,
        Type ModuleType,
        MethodInfo CommandMethod,
        IReadOnlyList<ParameterInfo> Parameters
    )
    {
        public static IEnumerable<RegisteredCommand> GetCommandsFromModuleType(Type commandModuleType)
        {
            var methods = commandModuleType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var commandAttr = method.GetCustomAttribute<CommandAttribute>();

                if (commandAttr != null)
                {
                    var methodParams = method.GetParameters();

                    yield return new RegisteredCommand(
                        commandAttr.Name,
                        commandAttr.Priority,
                        commandModuleType,
                        method,
                        methodParams
                    );
                }
            }
        }
    }
}
