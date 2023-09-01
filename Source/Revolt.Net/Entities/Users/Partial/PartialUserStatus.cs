using Revolt.Net.Common.Types;
using Revolt.Net.Enums;

namespace Revolt.Net.Entities.Users.Partials
{
    public sealed class PartialUserStatus
    {
        public Optional<string> Text { get; init; } = default!;

        public Optional<Presence> Presence { get; init; } = default!;
    }
}
