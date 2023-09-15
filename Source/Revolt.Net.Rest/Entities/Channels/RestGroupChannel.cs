using Revolt.Net.Rest.Clients;
using Revolt.Net.Rest.Entities.Channels;

namespace Revolt.Net.Rest
{
    public sealed class RestGroupChannel : RestMessageChannel, IMessageChannel
    {
        public string Name { get; private set; } = default!;

        public string OwnerId { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        internal RestGroupChannel(RevoltClientBase client, string id) : base(client, id)
        {

        }

        internal new static RestGroupChannel Create(RevoltClientBase client, ChannelPayload channel)
        {
            var chnl = new RestGroupChannel(client, channel.Id);

            chnl.Update(channel);

            return chnl;
        }

        internal override void Update(ChannelPayload channel)
        {
            base.Update(channel);

            channel.Name.Match(x => Name = x);
            channel.OwnerId.Match(x => OwnerId = x);
            channel.Description.Match(x => Description = x);
        }

        public async Task<IUser> GetOwnerAsync() =>
            await Client.Api.GetUserAsync(OwnerId);

        public bool IsOwner(IUser user) =>
            OwnerId == user.Id;
    }
}