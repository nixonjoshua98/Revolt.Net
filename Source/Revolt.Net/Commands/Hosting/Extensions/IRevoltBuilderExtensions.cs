using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Hosting.HostedServices;
using Revolt.Net.Commands.Services;
using Revolt.Net.Hosting.Builders;

namespace Revolt.Net.Commands.Hosting.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddCommandHandler<T>(this IRevoltBuilder builder) where T : class, ICommandMessageHandler
        {
            builder.Services.TryAddScoped<ICommandMessageHandler, T>();

            builder.Services.TryAddScoped<ICommandProcessor, RevoltCommandService>();

            builder.Services.AddHostedService<CommandHandlerBackgroundService>();

            return builder;
        }
    }
}
