using Revolt.Net.Core.Common.Types;

namespace Revolt.Net.Core.Entities.Users.Partials
{
    internal sealed class PartialUser
    {
        public Optional<PartialUserStatus> Status { get; init; } = default!;
    }
}
