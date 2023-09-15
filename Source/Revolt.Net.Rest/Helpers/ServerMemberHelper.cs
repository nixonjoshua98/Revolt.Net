using Revolt.Net.Rest.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Rest.Helpers
{
    internal static class ServerMemberHelper
    {
        public static IEnumerable<ServerMemberUser> CreateServerMemberUsers(
            IEnumerable<ServerMember> members,
            IEnumerable<RestUser> users,
            bool ignoreMissingUsers = true)
        {
            var ls = new List<ServerMemberUser>();

            for (int i = 0; i < members.Count(); i++)
            {
                var member = members.ElementAt(i);

                var user = users.FirstOrDefault(u => u.Id == member.Id);

                if (user is null && !ignoreMissingUsers)
                {
                    if (!ignoreMissingUsers)
                    {
                        throw new RevoltException($"User count does not match server member count");
                    }

                    continue;
                }

                var memberUser = ServerMemberUser.Create(member, user);

                ls.Add(memberUser);
            }

            return ls;
        }
    }
}
