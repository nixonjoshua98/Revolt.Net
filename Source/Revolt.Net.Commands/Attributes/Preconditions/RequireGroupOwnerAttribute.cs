﻿using Revolt.Net.Commands._Original.Results;
using Revolt.Net.Commands.Attributes;
using Revolt.Net.Commands.Context;
using Revolt.Net.Commands.Info;
using Revolt.Net.Entities.Channels;

namespace Revolt.Net.Commands.Attributes.Preconditions
{
    public class RequireGroupOwnerAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command,
            IServiceProvider services)
        {
            return context.Channel is GroupChannel channel && channel.OwnerId == context.Message.AuthorId ?
                Task.FromResult(PreconditionResult.FromSuccess()) :
                Task.FromResult(PreconditionResult.FromError("This command can only be ran by the owner of this group."));
        }
    }
}