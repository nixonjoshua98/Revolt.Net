namespace Revolt.Net
{
    public interface IServer
    {
        string Id { get; init; }

        ValueTask<IUser> GetOwnerAsync();
        ServerMember GetServerMember(string userId);
        Task<IEnumerable<ServerMember>> GetServerMembersAsync(bool excludeOffline = false);
    }
}