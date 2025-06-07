using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestGroupChannel : RestTextChannel
    {
        public string Name { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        public string Description { get; init; } = default!;

        public bool IsOwner(IUser user) =>
            OwnerId == user.Id;
    }
}