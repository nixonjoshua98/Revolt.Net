using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestGroupChannel : RestMessageChannel
    {
        public string Name { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        public string Description { get; init; } = default!;

        public async Task<IUser> GetOwnerAsync() =>
            await Client.Api.GetUserAsync(OwnerId);

        public bool IsOwner(IUser user) =>
            OwnerId == user.Id;
    }
}