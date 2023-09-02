using Revolt.Net.Core.Common.Types;

namespace Revolt.Net.Entities.Users.Partial
{
    public sealed class PartialUserStatus
    {
        public Optional<string> Text { get; init; } = default!;

        public Optional<Presence> Presence { get; init; } = default!;
    }
}
