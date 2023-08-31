using Revolt.Net.Core;
using Revolt.Net.Core.Common.Exceptions;
using Revolt.Net.Core.Entities;
using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Rest;
using Revolt.Net.Rest.API;
using Revolt.Net.State;
using Revolt.Net.Websocket;

namespace Revolt.Net.Clients;

public sealed class RevoltBotClient : RevoltClientBase
{
    internal readonly RevoltBotConfiguration Configuration;
    internal readonly IRevoltStateCache Cache;
    internal readonly RevoltApiClient Api;
    internal readonly RevoltState State;
    internal User User;

    private RevoltApiInformation ApiInfo = default!;
    internal RevoltWebsocketConnection Ws = default!;

    public RevoltBotClient(
        RevoltBotConfiguration configuration,
        IRevoltStateCache cacheProvider)
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

    public RevoltBotClient(RevoltBotConfiguration configuration) : this(configuration, new DefaultRevoltStateCache()) { }

    public RevoltBotClient(string token) : this(new RevoltBotConfiguration() { Token = token }) { }

    public async Task LogoutAsync(CancellationToken ct = default)
    {
        await Ws.DisconnectAsync(ct);
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        ApiInfo ??= await Api.GetApiInformationAsync() ?? throw new RevoltException("Failed to load Revolt api information");

        User = await Api.GetSelfUserAsync();

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
        User ??= Api.GetSelfUserAsync().Result;

        return User.Bot.OwnerId == id;
    }
}
