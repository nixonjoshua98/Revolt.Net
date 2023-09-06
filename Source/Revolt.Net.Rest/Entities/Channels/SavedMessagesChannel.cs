using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public class SavedMessagesChannel : Channel
    {
        [JsonPropertyName("user")]
        public string UserId { get; init; } = default!;

        public async ValueTask<IUser> GetUserAsync(FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await Client.GetUserAsync(UserId, behaviour);
    }
}