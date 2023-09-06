using Revolt.Net.Rest;
using Revolt.Net.Rest.Entities;
using Revolt.Net.Rest.Entities.Users;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket
{
    public sealed class RevoltSocketClient : RevoltSocketClientBase
    {
        internal readonly RevoltBotConfiguration Configuration;
        internal readonly IRevoltStateCache Cache;
        internal readonly RevoltApiClient Api;
        internal readonly RevoltState State;

        internal RestClientUser User;

        private RevoltApiInformation ApiInfo = default!;
        internal RevoltWebSocketConnection Ws = default!;

        public RevoltSocketClient(RevoltBotConfiguration configuration, IRevoltStateCache cacheProvider) : base()
        {
            Configuration = configuration;
            Cache = cacheProvider;

            Api = new RevoltApiClient(
                new RevoltRestClient(configuration.ApiUrl)
            );

            Api.Client.AddDefaultHeader("x-bot-token", Configuration.Token);

            State = new RevoltState(this);
        }

        public RevoltSocketClient(RevoltBotConfiguration configuration) : this(configuration, new DefaultRevoltStateCache()) { }

        public RevoltSocketClient(string token) : this(new RevoltBotConfiguration() { Token = token }) { }

        public async Task LogoutAsync(CancellationToken ct = default)
        {
            await Ws.DisconnectAsync(ct);
        }

        public async Task RunAsync(CancellationToken ct = default)
        {
            ApiInfo ??= await Api.GetApiInformationAsync() ?? throw new RevoltException("Failed to load Revolt api information");

            User = await Api.GetClientUserAsync();

            CreateSocket();

            await Ws.RunAsync(ct);
        }

        private void CreateSocket()
        {
            bool isNewConnection = Ws is null;

            Ws = new RevoltWebSocketConnection(ApiInfo.WebsocketUrl, Configuration.Token);

            if (isNewConnection)
            {
                var consumer = new RevoltWebSocketConsumer(Ws, State);

                SetupEvents(consumer);
            }
        }

        public SocketUser GetUserByName(string name) => State.GetUser(name);
        public async Task<SocketUser> GetUserAsync(string id) => await State.GetUserAsync(id);
        public SocketServer GetServer(string id) => State.GetServer(id);
        public SocketChannel GetChannel(string id) => State.GetChannel(id);
        public SocketUser GetUser(string id) => State.GetUser(id);

        public bool IsOwner(string id)
        {
            return User.Bot.Match(x => x.OwnerId == id, false);
        }
    }
}