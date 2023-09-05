namespace Revolt.Net.Rest.Entities.Users
{
    public sealed class RestClientUser : IUser
    {
        public string Id { get; init; }

        public string Username { get; init; }

        public string Discriminator { get; init; }

        public Optional<UserBot> Bot { get; init; }
    }
}
