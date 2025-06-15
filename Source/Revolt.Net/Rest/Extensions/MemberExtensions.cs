using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Rest.Contracts.RestValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
