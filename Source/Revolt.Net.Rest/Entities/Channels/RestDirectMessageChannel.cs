using Revolt.Net.Rest.Clients;
using Revolt.Net.Rest.Entities.Channels;

namespace Revolt.Net.Rest
{
    public class RestDirectMessageChannel : RestMessageChannel
    {
        public bool Active { get; private set; }
        public IEnumerable<string> Recipients { get; private set; }

        internal RestDirectMessageChannel(RevoltClientBase client, ChannelPayload channel) : base(client, channel.Id)
        {

        }

        internal override void Update(ChannelPayload channel)
        {
            base.Update(channel);

            channel.Active.Match(x => Active = x);
            channel.Recipients.Match(x => Recipients = x);

        }

        internal new static RestDirectMessageChannel Create(RevoltClientBase client, ChannelPayload channel)
        {
            var chnl = new RestDirectMessageChannel(client, channel);

            chnl.Update(channel);

            return chnl;
        }
    }
}