using Revolt.Net.Commands.Models;

namespace Revolt.Net.Commands.Configuration
{
    internal sealed class RevoltCommandState
    {
        public IReadOnlyList<RegisteredCommand> Commands { get; set; } = [];

        public bool TryGetCommandByName(string name, out RegisteredCommand command)
        {
            command = Commands.FirstOrDefault(x => x.Command == name)!;

            return command != null;
        }
    }
}
