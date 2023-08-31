using Revolt.Net.Core;
using Revolt.Net.Core.Common;
using Revolt.Net.Core.Entities;
using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Rest;
using Revolt.Net.Rest.API;
using Revolt.Net.State;
using Revolt.Net.Websocket;
using Revolt.Net.Websocket.Events.Incoming;

namespace Revolt.Net;

public sealed class RevoltClient
{
    internal readonly RevoltApiClient Api;

    private RevoltApiInformation ApiInfo = default!;

    internal RevoltState State = default!;
    internal RevoltWebsocketConnection Ws = default!;

    internal AsyncEvent<ReadyInternalEvent> _Ready { get; } = new();
    internal AsyncEvent<UserUpdateInternalEvent> _UserUpdated { get; } = new();
    internal AsyncEvent<ChannelCreateInternalEvent> _ChannelCreated { get; } = new();
    internal AsyncEvent<UserRelationshipInternalEvent> _RelationshipUpdated { get; } = new();

    public AsyncEvent<ChannelDeleteEvent> ChannelDeleted { get; } = new();
    public AsyncEvent<ServerDeleteEvent> ServerDeleted { get; } = new();
    public AsyncEvent<ReadyEvent> Ready { get; } = new();
    public AsyncEvent<MessageEvent> Message { get; } = new();
    public AsyncEvent<MessageDeleteEvent> MessageDeleted { get; } = new();
    public AsyncEvent<UserUpdateEvent> UserUpdated { get; } = new();


    public RevoltClient() : this(RevoltConfiguration.Default)
    {

    }

    public RevoltClient(RevoltConfiguration configuration)
    {
        Api = new RevoltApiClient(
            new RevoltRestClient(configuration)
        );
    }

    public async Task LoginAsync(string token)
    {
        Api.Client.AddDefaultHeader("x-bot-token", token);

        ApiInfo = await Api.GetApiInformationAsync();

        Ws = new RevoltWebsocketConnection(this, ApiInfo.WebsocketUrl, token);

        State = new RevoltState(
            new DefaultRevoltStateCache(),
            Api,
            this
        );

        _ = new RevoltEventConsumer(this);
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        await Ws.RunAsync(ct);
    }

    public Server? GetServer(string id) => State.GetServer(id);

    public async Task<User?> GetUserAsync(string id) => await State.GetUserAsync(id);
}
