using Revolt.Net.Core.Common.Types;

namespace Revolt.Net.Entities.Users.Partial
{
    public sealed class PartialUser
    {
        public Optional<PartialUserStatus> Status { get; init; } = default!;
    }
}
