namespace Revolt.Net.Rest
{
    public interface IRestTextChannel : IMessageChannel
    {
        Task<RestMessagesResponse> GetMessagesAsync(int limit, string beforeMessageId = null, string afterMessageId = null, MessageSort sort = MessageSort.Relevance, string nearbyMessageId = null, bool includeUsers = true);
    }
}