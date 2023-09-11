using Revolt.Net.Commands.Enums;
using Revolt.Net.Commands.Results;

namespace Revolt.Net.Commands.Readers
{
    /// <summary>
    ///     A <see cref="TypeReader"/> for parsing objects implementing <see cref="IChannel"/>.
    /// </summary>
    /// <remarks>
    ///     This <see cref="TypeReader"/> is shipped with Revolt.Net and is used by default to parse any 
    ///     <see cref="IChannel"/> implemented object within a command. The TypeReader will attempt to first parse the
    ///     input by mention, then the snowflake identifier, then by name; the highest candidate will be chosen as the
    ///     final output; otherwise, an erroneous <see cref="TypeReaderResult"/> is returned.
    /// </remarks>
    /// <typeparam name="T">The type to be checked; must implement <see cref="IChannel"/>.</typeparam>
    internal sealed class ChannelReader<T> : TypeReader where T : class, IChannel
    {
        /// <inheritdoc />
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            var results = new Dictionary<string, TypeReaderValue>();

            // Identifer
            var fetchedChannel = await context.Client.GetChannelAsync(input);

            if (fetchedChannel is T chnl)
                AddResult(results, chnl, 9f);

            if (results.Count > 0)
                return TypeReaderResult.FromSuccess(results.Values);

            return TypeReaderResult.FromError(CommandError.ObjectNotFound, "Channel not found.");
        }

        private void AddResult(Dictionary<string, TypeReaderValue> results, T channel, float score)
        {
            if (channel is not null && !results.ContainsKey(channel.Id))
                results.Add(channel.Id, new TypeReaderValue(channel, score));
        }
    }
}
