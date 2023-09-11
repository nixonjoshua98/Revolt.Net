namespace Revolt.Net
{
    public interface IServer
    {
        string Id { get; init; }

        Task<IUser> GetOwnerAsync();
        Task<IEnumerable<ServerMemberUser>> GetServerMembersAsync(bool excludeOffline = false);
    }
}