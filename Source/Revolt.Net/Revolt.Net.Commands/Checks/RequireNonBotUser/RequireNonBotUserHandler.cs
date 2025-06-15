namespace Revolt.Net.Commands.Checks.RequireNonBotUser
{
    internal sealed class RequireNonBotUserHandler : ICommandPreCheckHandler<RequireNonBotUserAttribute>
    {
        public Task<CommandPreCheckResult> CheckAsync(CommandContext context, RequireNonBotUserAttribute check, CancellationToken cancellationToken)
        {
            var result = context.Message.Author.IsBot ?
                CommandPreCheckResult.Failed() :
                CommandPreCheckResult.Success();

            return Task.FromResult(result);
        }
    }
}
