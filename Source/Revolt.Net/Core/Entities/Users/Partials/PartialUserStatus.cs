using Revolt.Net.Core.Common.Types;
using Revolt.Net.Core.Enums;

namespace Revolt.Net.Core.Entities.Users.Partials
{
    public sealed class PartialUserStatus
    {
        public Optional<string> Text { get; init; } = default!;

        public Optional<Presence> Presence { get; init; } = default!;
    }
}
