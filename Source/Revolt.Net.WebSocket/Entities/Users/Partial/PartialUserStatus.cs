using Revolt.Net.Core;

namespace Revolt.Net.WebSocket
{
    public sealed class PartialUserStatus
    {
        public Optional<string> Text { get; init; } = default!;

        public Optional<Presence> Presence { get; init; } = default!;
    }
}
