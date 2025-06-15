using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Builders;
using Revolt.Net.Commands.Hosting.HostedServices;
using Revolt.Net.Commands.Models;
using Revolt.Net.Commands.Services;
using Revolt.Net.Commands.TypeBinding.DefaultBinders;
using Revolt.Net.Core.Hosting.Builders;

namespace Revolt.Net.Commands.Hosting.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddCommands<T>(
            this IRevoltBuilder builder,
            Action<IRevoltCommandsBuilder>? builderAction = null
        ) where T : class, ICommandMessageHandler
        {
            var commandsBuilder = new RevoltCommandsBuilder(builder)
                .AddTypeParser<int, DefaultIntTypeParser>()
                .AddTypeParser<DefaultEnumTypeParser>();

            builder.Services.TryAddScoped<ICommandMessageHandler, T>();

            builder.Services.TryAddScoped<ICommandProcessor, CommandProcessor>();

            builder.Services.AddHostedService<CommandHandlerBackgroundService>();

            builder.Services.TryAddSingleton(p =>
                p.GetRequiredService<IOptions<RevoltCommandStateBuilder>>().Value.Build());

            builder.Services.Configure<RevoltCommandStateBuilder>(_ => { });

            builderAction?.Invoke(commandsBuilder);

            return builder;
        }
    }
}
