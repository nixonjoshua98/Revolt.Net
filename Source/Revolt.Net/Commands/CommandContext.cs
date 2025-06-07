using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Core.Entities.Messages;

namespace Revolt.Net.Commands
{
    public sealed record CommandContext(Message Message) : ICommandContext;
}
