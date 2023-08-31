﻿using Revolt.Net.Core.Entities.Common;
using Revolt.Net.Core.Entities.Users;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Servers
{
    public sealed class Server : RevoltEntity
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        [JsonPropertyName("channels")]
        public List<string> ChannelIds { get; init; } = new();

        public IEnumerable<ServerMember> Members => Client.State.GetServerMembers(Id);

        public ServerMember? GetServerMember(string userId) => Client.State.GetServerMember(Id, userId);

        public async Task<IEnumerable<ServerMember>> GetServerMembersAsync(bool excludeOffline = false)
        {
            var resp = await Client.Api.GetServerMembersAsync(Id, excludeOffline);

            Client.State.Add(Id, resp);

            return Members;
        }
    }
}
