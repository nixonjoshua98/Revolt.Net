namespace Revolt.Net.Rest.Clients
{
    public abstract class RevoltClientBase
    {
        public RevoltClientBase(string apiUrl)
        {
            Api = new RevoltApiClient(
                new RevoltHttpClient(apiUrl)
            );
        }

        internal RevoltApiClient Api { get; private set; }

        public IUser User { get; protected set; }

        public abstract Task<IUser> GetUserAsync(string id);
        public abstract Task<IChannel> GetChannelAsync(string id);

        public bool IsOwner(string id) =>
            User.Bot.Match(x => x.OwnerId == id, false);

    }
}
