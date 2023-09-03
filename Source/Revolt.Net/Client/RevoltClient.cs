using Revolt.Net.Entities;
using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Servers;
using Revolt.Net.Entities.Users;
using Revolt.Net.Exceptions;
using Revolt.Net.Rest;
using Revolt.Net.Rest.ApiClient;
using Revolt.Net.State;
using Revolt.Net.Websocket;

namespace Revolt.Net.Client;

public sealed class RevoltClient : RevoltClientBase
{
    internal readonly RevoltBotConfiguration Configuration;
    internal readonly IRevoltStateCache Cache;
    internal readonly RevoltApiClient Api;
    internal readonly RevoltState State;
    internal User User;

    private RevoltApiInformation ApiInfo = default!;
    internal RevoltWebsocketConnection Ws = default!;

    public RevoltClient(RevoltBotConfiguration configuration, IRevoltStateCache cacheProvider) : base()
    {
        Configuration = configuration;
        Cache = cacheProvider;

        Api = new RevoltApiClient(
            new RevoltRestClient(configuration)
        );

        Api.Client.AddDefaultHeader("x-bot-token", Configuration.Token);

        State = new RevoltState(this);

        _ = new RevoltWebsocketConsumer(this);
    }

    public RevoltClient(RevoltBotConfiguration configuration) : this(configuration, new DefaultRevoltStateCache()) { }

    public RevoltClient(string token) : this(new RevoltBotConfiguration() { Token = token }) { }

    public async Task LogoutAsync(CancellationToken ct = default)
    {
        await Ws.DisconnectAsync(ct);
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        ApiInfo ??= await Api.GetApiInformationAsync() ?? throw new RevoltException("Failed to load Revolt api information");

        User = await Api.GetClientUserAsync();

        Ws = new RevoltWebsocketConnection(this, ApiInfo.WebsocketUrl, Configuration.Token);

        await Ws.RunAsync(ct);
    }

    public User GetUserByName(string name) => State.GetUser(name);
    public async Task<User> GetUserAsync(string id) => await State.GetUserAsync(id);
    public Server GetServer(string id) => State.GetServer(id);
    public Channel GetChannel(string id) => State.GetChannel(id);
    public User GetUser(string id) => State.GetUser(id);

    public bool IsOwner(string id)
    {
        User ??= Api.GetClientUserAsync().Result;

        return User.Bot.OwnerId == id;
    }
}
