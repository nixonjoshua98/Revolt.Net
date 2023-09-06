using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public sealed class GroupChannel : MessageChannel
    {
        public string Name { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        public string Description { get; init; } = default!;

        public async ValueTask<IUser> GetOwnerAsync(FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await Client.GetUserAsync(OwnerId, behaviour);

        public bool IsOwner(IUser user) =>
            OwnerId == user.Id;
    }
}