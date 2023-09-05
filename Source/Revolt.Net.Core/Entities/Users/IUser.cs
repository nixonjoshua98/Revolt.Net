namespace Revolt.Net.Core.Entities.Users
{
    public interface IUser
    {
        string Id { get; init; }
        string Username { get; init; }
        Optional<UserBot> Bot { get; init; }
    }
}
