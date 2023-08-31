using Revolt.Net.Core;
using Revolt.Net.Core.Clients;
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

public sealed class RevoltClient : RevoltClientBase, IRevolutClient
{
    internal readonly RevoltBotConfiguration Configuration;
    internal readonly IRevoltStateCache Cache;
    internal readonly RevoltApiClient Api;
    internal readonly RevoltState State;

    private RevoltApiInformation ApiInfo = default!;
    internal RevoltWebsocketConnection Ws = default!;

    public RevoltClient(
        RevoltBotConfiguration configuration,
        IRevoltStateCache cacheProvider)
    {
        Configuration = configuration;
        Cache = cacheProvider;

        Api = new RevoltApiClient(
            new RevoltRestClient(configuration)
        );

        State = new RevoltState(this);
    }

    public async Task LoginAsync(CancellationToken ct = default)
    {
        Api.Client.AddDefaultHeader("x-bot-token", Configuration.Token);

        ApiInfo = await Api.GetApiInformationAsync() ?? throw new RevolutException("Failed to load Revolt api information");

        Ws = new RevoltWebsocketConnection(this, ApiInfo.WebsocketUrl, Configuration.Token);

        _ = new RevoltEventConsumer(this);
    }

    public async Task LogoutAsync(CancellationToken ct = default)
    {

    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        await Ws.RunAsync(ct);
    }

    public async Task<User?> GetUserAsync(string id) => await State.GetUserAsync(id);

    public Server? GetServer(string id) => State.GetServer(id);

    public Channel? GetChannel(string id) => State.GetChannel(id);

    public User? GetUser(string id) => State.GetUser(id);
}
