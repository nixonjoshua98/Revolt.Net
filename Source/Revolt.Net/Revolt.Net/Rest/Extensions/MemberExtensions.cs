using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Rest.Contracts.RestValues;

namespace Revolt.Net.Rest.Extensions
{
    public static class MemberExtensions
    {
        public static async Task<User> GetUserAsync(this ServerMember member, CancellationToken cancellationToken = default)
        {
            var values = new GetUserValues(member.UserId);

            return await member.Client.GetUserAsync(values, cancellationToken);
        }
    }
}
