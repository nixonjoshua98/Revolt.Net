using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Core.JsonModels.Messages;
using Revolt.Net.Rest.Contracts.RestValues;

namespace Revolt.Net.Rest.Extensions
{
    public static class MessageExtensions
    {
        public static async Task<Message> ReplyAsync(
            this Message message, 
            string? content = null,
            bool mention = false,
            bool failIfNotExists = true,
            IEnumerable<Embed>? embeds = null,
            CancellationToken cancellationToken = default)
        {
            var req = new SendMessageValues(
                message.ChannelId, 
                content, 
                embeds ?? [],
                [ new JsonMessageReply(message.Id, mention, failIfNotExists) ]
            );

            return await message.Client.SendMessageAsync(req, cancellationToken);
        }

        public static async Task DeleteAsync(
            this Message message,
            CancellationToken cancellationToken = default)
        {
            var req = new DeleteMessageValues(
                message.ChannelId,
                message.Id
            );

            await message.Client.DeleteMessageAsync(req, cancellationToken);
        }

        public static async Task<Message> EditAsync(
            this Message message,
            string? content = null,
            IEnumerable<Embed>? embeds = null,
            CancellationToken cancellationToken = default)
        {
            var req = new EditMessageValues(
                message.ChannelId,
                message.Id,
                content,
                embeds
            );

            return await message.Client.EditMessageAsync(req, cancellationToken);
        }

        public static async Task PinAsync(
            this Message message,
            CancellationToken cancellationToken = default)
        {
            var req = new PinMessageValues(
                message.ChannelId,
                message.Id
            );

            await message.Client.PinMessageAsync(req, cancellationToken);
        }

        public static async Task UnPinAsync(
            this Message message,
            CancellationToken cancellationToken = default)
        {
            var req = new PinMessageValues(
                message.ChannelId,
                message.Id
            );

            await message.Client.UnPinMessageAsync(req, cancellationToken);
        }

        public static async Task<Channel> GetChannelAsync(
            this Message message,
            CancellationToken cancellationToken = default)
        {
            var req = new GetChannelValues(
                message.ChannelId
            );

            return await message.Client.GetChannelAsync(req, cancellationToken);
        }

        public static async Task RefreshAsync(
            this Message message,
            CancellationToken cancellationToken = default)
        {
            var req = new GetMessageValues(
                message.ChannelId,
                message.Id
            );

            var refreshed = await message.Client.GetMessageAsync(req, cancellationToken);

            message.UpdateJsonModel(refreshed.JsonModel);
        }
    }
}
