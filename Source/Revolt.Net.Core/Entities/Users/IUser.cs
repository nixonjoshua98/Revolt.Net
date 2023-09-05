using Revolt.Net.Core;

namespace Revolt.Net
{
    public interface IUser
    {
        string Id { get; init; }
        string Username { get; init; }
        Optional<UserBot> Bot { get; init; }
    }
}
