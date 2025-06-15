using Revolt.Net.Commands.Attributes;
using Revolt.Net.Commands.Checks;
using System.Reflection;

namespace Revolt.Net.Commands.Models
{
    internal sealed record RevoltCommandPreCheckDataDescriptor(ICommandPreCheck CheckData, Type CheckType)
    {
        public static RevoltCommandPreCheckDataDescriptor FromCheck(ICommandPreCheck data) => new RevoltCommandPreCheckDataDescriptor(data, data.GetType());
    }

    internal sealed record RevoltCommandDescriptor(
        string Command,
        int Priority,
        Type ModuleType,
        MethodInfo CommandMethod,
        IReadOnlyList<ParameterInfo> Parameters,
        IReadOnlyList<RevoltCommandPreCheckDataDescriptor> PreChecks
    )
    {
        public static IEnumerable<RevoltCommandDescriptor> GetCommandsFromModuleType(Type commandModuleType)
        {
            var methods = commandModuleType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var commandAttr = method.GetCustomAttribute<CommandAttribute>();

                if (commandAttr != null)
                {
                    var methodParams = method.GetParameters();

                    var preChecks = method.GetCustomAttributes()
                        .OfType<ICommandPreCheck>()
                        .Select(RevoltCommandPreCheckDataDescriptor.FromCheck)
                        .ToList();

                    yield return new RevoltCommandDescriptor(
                        commandAttr.Name,
                        commandAttr.Priority,
                        commandModuleType,
                        method,
                        methodParams,
                        preChecks
                    );
                }
            }
        }
    }
}
