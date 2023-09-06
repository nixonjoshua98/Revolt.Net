using Revolt.Net.WebSocket.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.Helpers
{
    internal static class UserHelper
    {
        public static async ValueTask<SocketUser> GetUserAsync(
            RevoltSocketClient client,
            string userId,
            FetchBehaviour behaviour)
        {
            return await FetchHelper.GetOrDownloadAsync(
                behaviour,
                () => client.State.GetUser(userId),
                () => client.Api.GetUserAsync(userId),
                client.State.AddUser
            );
        }
    }
}
