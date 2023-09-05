using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Converters
{
    internal class RevoltChannelConverter : JsonConverter<Channel>
    {
        public override Channel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            var ele = doc.RootElement;

            var json = ele.GetRawText();
            var node = JsonNode.Parse(json);

            var channelType = Enum.Parse<ChannelType>(node!["channel_type"]!.ToString());

            return channelType switch
            {
                ChannelType.TextChannel => node.Deserialize<TextChannel>(options),
                ChannelType.Group => node.Deserialize<GroupChannel>(options),
                ChannelType.DirectMessage => node.Deserialize<DirectMessageChannel>(options),
                ChannelType.SavedMessages => node.Deserialize<SavedMessagesChannel>(options),
                _ => throw new Exception()
            }; ;
        }

        public override void Write(Utf8JsonWriter writer, Channel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
