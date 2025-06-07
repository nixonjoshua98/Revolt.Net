namespace Revolt.Net.Commands.Abstractions
{
    public abstract class CommandModule<TContext> where TContext : ICommandContext
    {

    }

    public abstract class CommandModule : CommandModule<CommandContext>
    {

    }
}
