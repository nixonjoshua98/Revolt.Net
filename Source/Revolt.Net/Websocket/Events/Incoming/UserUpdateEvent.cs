using Revolt.Net.Entities.Users.Partial;
using System.Text.Json.Serialization;

namespace Revolt.Net.Websocket.Events.Incoming;

public sealed record UserUpdateEvent(string UserId);

internal sealed class UserUpdateInternalEvent
{
    [JsonPropertyName("id")]
    public string UserId { get; init; } = default!;

    [JsonPropertyName("data")]
    public PartialUser User { get; init; } = default!;

    internal UserUpdateEvent ToPublicEvent() => new(UserId);
}
