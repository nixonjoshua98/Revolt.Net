using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Configuration;
using Revolt.Net.Commands.Hosting.HostedServices;
using Revolt.Net.Commands.Models;
using Revolt.Net.Commands.Services;
using Revolt.Net.Hosting.Builders;

namespace Revolt.Net.Commands.Hosting.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddCommandHandler<T>(this IRevoltBuilder builder) where T : class, ICommandMessageHandler
        {
            builder.Services.TryAddScoped<ICommandMessageHandler, T>();

            builder.Services.TryAddScoped<ICommandProcessor, CommandProcessor>();

            builder.Services.AddHostedService<CommandHandlerBackgroundService>();

            builder.Services.Configure<RevoltCommandState>(cfg =>
            {
                cfg.Commands = LoadCommands();
            });

            return builder;
        }

        private static List<RegisteredCommand> LoadCommands()
        {
            var commandModuleType = typeof(CommandModule);

            var modules = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.DefinedTypes)
                .Where(t => t.IsAssignableTo(commandModuleType));

            List<RegisteredCommand> commands = [];

            foreach (var module in modules)
            {
                var moduleCommands = RegisteredCommand.GetCommandsFromModuleType(module);

                commands.AddRange(moduleCommands);
            }

            return commands;
        }
    }
}
