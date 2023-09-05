using Revolt.Net.Core;

namespace Revolt.Net.WebSocket
{
    public sealed class PartialUser
    {
        public Optional<PartialUserStatus> Status { get; init; } = default!;
    }
}
