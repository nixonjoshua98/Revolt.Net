using Revolt.Net.Rest.Clients;
using Revolt.Net.Rest.Entities.Channels;

namespace Revolt.Net.Rest
{
    public class RestTextChannel : RestMessageChannel, IRestTextChannel
    {
        public string ServerId { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string LastMessageId { get; private set; }
        public bool Nsfw { get; private set; }

        internal RestTextChannel(RevoltClientBase client, string id, string serverId) : base(client, id)
        {
            ServerId = serverId;
        }

        internal override void Update(ChannelPayload channel)
        {
            base.Update(channel);

            channel.Name.Match(x => Name = x);
            channel.Description.Match(x => Description = x);
            channel.LastMessageId.Match(x => LastMessageId = x);
            channel.Nsfw.Match(x => Nsfw = x);
        }

        internal new static RestTextChannel Create(RevoltClientBase client, ChannelPayload channel)
        {
            var chnl = new RestTextChannel(client, channel.Id, channel.ServerId.Value);

            chnl.Update(channel);

            return chnl;
        }
    }
}