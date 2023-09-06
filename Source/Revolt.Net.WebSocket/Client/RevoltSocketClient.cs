using Revolt.Net.WebSocket.Helpers;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket
{
    public sealed class RevoltSocketClient : RevoltSocketClientBase
    {
        internal readonly RevoltBotConfiguration Configuration;
        internal readonly IRevoltStateCache Cache;
        internal readonly RevoltState State;

        private RevoltApiInformation ApiInfo = default!;
        internal RevoltWebSocketConnection Ws = default!;

        public RevoltSocketClient(RevoltBotConfiguration configuration, IRevoltStateCache cacheProvider) : base(configuration.ApiUrl)
        {
            Configuration = configuration;
            Cache = cacheProvider;

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
                var consumer = new RevoltWebSocketConsumer(this, Ws, State);

                SetupEvents(consumer);
            }
        }

        public override async ValueTask<IUser> GetUserAsync(string id, FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await UserHelper.GetUserAsync(this, id, behaviour);

        public override async ValueTask<IChannel> GetChannelAsync(string id, FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await ChannelHelper.GetChannelAsync(this, id, behaviour);

        public SocketServer GetServer(string id) => 
            State.GetServer(id);

        public Channel GetChannel(string id) => 
            State.GetChannel(id);

        public IUser GetUser(string id) => 
            State.GetUser(id);
    }
}