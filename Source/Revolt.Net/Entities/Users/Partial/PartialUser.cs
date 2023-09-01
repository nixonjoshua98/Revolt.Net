using Revolt.Net.Common.Types;

namespace Revolt.Net.Entities.Users.Partials
{
    public sealed class PartialUser
    {
        public Optional<PartialUserStatus> Status { get; init; } = default!;
    }
}
