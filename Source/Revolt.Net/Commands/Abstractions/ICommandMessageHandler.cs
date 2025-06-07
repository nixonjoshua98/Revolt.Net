namespace Revolt.Net.Commands.Abstractions
{
    public interface ICommandMessageHandler
    {
        Task HandleAsync(CommandContext context, CancellationToken cancellationToken);
    }
}
