using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Commands.Checks.Defaults.MustNotBeBot;
using Revolt.Net.Core.Exceptions;
using System.Reflection;

namespace Revolt.Net.Commands.Checks
{
    internal sealed record RevoltCommandPreCheckDescriptor(Type PreCheckType, Type HandlerType, MethodInfo HandleMethod)
    {
        public async Task<CommandPreCheckResult> CheckAsync(CommandContext context, object data, IServiceProvider provider, CancellationToken cancellationToken)
        {
            var handler = ActivatorUtilities.GetServiceOrCreateInstance(provider, HandlerType);

            var resultReturn = HandleMethod.Invoke(handler, [context, data, cancellationToken]);

            var resultTask = resultReturn as Task<CommandPreCheckResult> ?? throw new RevoltException("Pre-check failed due to returning null");

            return await resultTask;
        }

        public static RevoltCommandPreCheckDescriptor FromHandler(Type checkType, Type handlerType)
        {
            var checkMethod = handlerType.GetMethod(nameof(ICommandPreCheckHandler<>.CheckAsync))
                ?? throw new RevoltException("Command pre-check handler is missing required method");

            return new RevoltCommandPreCheckDescriptor(checkType, handlerType, checkMethod);
        }
    }

}
