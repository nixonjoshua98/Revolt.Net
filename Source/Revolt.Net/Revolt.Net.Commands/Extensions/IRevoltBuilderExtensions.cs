using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Builders;
using Revolt.Net.Commands.Services;
using Revolt.Net.Core.Builders;

namespace Revolt.Net.Commands.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        /// <summary>
        /// Adds the command processing system to the Revolt builder pipeline,
        /// including type parsers, check handlers, and command processor service registrations.
        /// </summary>
        /// <param name="builder">The Revolt builder instance to configure.</param>
        /// <param name="builderAction">
        /// Optional configuration action to customize the commands builder,
        /// allowing registration of additional modules, checks, or type parsers.
        /// </param>
        /// <returns>The original builder instance for chaining.</returns>
        public static IRevoltBuilder AddCommands(this IRevoltBuilder builder, Action<IRevoltCommandsBuilder>? builderAction = null)
        {
            var commandsBuilder = new RevoltCommandsBuilder(builder)
                .AddTypeParsersFromAssemblyContaining<CommandProcessor>()
                .AddCheckHandlersFromAssemblyContaining<CommandProcessor>();

            builder.Services.AddTransient<IRevoltCommandProcessor, CommandProcessor>();

            // Register the built command state as a singleton, constructed from configured options
            builder.Services.TryAddSingleton(p =>
                p.GetRequiredService<IOptions<RevoltCommandStateBuilder>>().Value.Build());

            // Register the command state builder configuration to enable further customization by users
            builder.Services.Configure<RevoltCommandStateBuilder>(_ => { });

            builderAction?.Invoke(commandsBuilder);

            return builder;
        }
    }

}
