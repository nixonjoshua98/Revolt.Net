namespace Revolt.Net.WebSocket.Helpers
{
    internal static class UserHelper
    {
        public static async ValueTask<IUser> GetUserAsync(
            RevoltSocketClient client,
            string userId,
            FetchBehaviour behaviour)
        {
            return await FetchHelper.GetOrDownloadAsync(
                behaviour,
                () => client.State.GetUser(userId),
                () => client.Api.GetUserAsync(userId)
            );
        }
    }
}
