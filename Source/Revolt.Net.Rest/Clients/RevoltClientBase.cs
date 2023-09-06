using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Rest.Clients
{
    public abstract class RevoltClientBase
    {
        public RevoltClientBase(string apiUrl)
        {
            Api = new RevoltApiClient(
                new RevoltRestClient(apiUrl)
            );
        }

        internal RevoltApiClient Api { get; private set; }

        public IUser User { get; protected set; }

        public abstract ValueTask<IUser> GetUserAsync(string id, FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload);
        public abstract ValueTask<IChannel> GetChannelAsync(string id, FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload);

        public bool IsOwner(string id) =>
            User.Bot.Match(x => x.OwnerId == id, false);

    }
}
