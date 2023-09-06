using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Converters
{
    internal class RevoltChannelConverter : JsonConverter<SocketChannel>
    {
        public override SocketChannel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            var ele = doc.RootElement;

            var json = ele.GetRawText();
            var node = JsonNode.Parse(json);

            var channelType = Enum.Parse<ChannelType>(node!["channel_type"]!.ToString());

            return channelType switch
            {
                ChannelType.TextChannel => node.Deserialize<SocketTextChannel>(options),
                ChannelType.Group => node.Deserialize<SocketGroupChannel>(options),
                ChannelType.DirectMessage => node.Deserialize<SocketDirectMessageChannel>(options),
                ChannelType.SavedMessages => node.Deserialize<SocketSavedMessagesChannel>(options),
                _ => throw new Exception()
            }; ;
        }

        public override void Write(Utf8JsonWriter writer, SocketChannel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
