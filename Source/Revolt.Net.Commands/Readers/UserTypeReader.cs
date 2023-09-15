using Revolt.Net.Commands.Enums;
using Revolt.Net.Commands.Results;
using System.Collections.Immutable;

namespace Revolt.Net.Commands.Readers
{
    public class UserTypeReader<T> : TypeReader where T : class, IUser
    {
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input,
            IServiceProvider services)
        {
            var results = new Dictionary<string, TypeReaderValue>();
            string id = null;
            // By mention and id (1.0)
            if (input.Length == 29 && input.StartsWith("<@") && input.EndsWith(">"))
                id = input[2..^1];
            else if (input.Length == 26)
                id = input;
            if (id != null)
            {
                var res = await context.Client.GetUserAsync(id);

                AddResult(results, res as T, 1.0f);
            }

            // By Username (0.9) TODO
            //AddResult(results, context.Client.GetUserByName(input) as T, 0.9f);
            // todo: By Nickname

            if (results.Count != 0)
                return TypeReaderResult.FromSuccess(results.Values.ToImmutableArray());

            return TypeReaderResult.FromError(CommandError.ObjectNotFound, "User not found.");
        }

        private void AddResult(Dictionary<string, TypeReaderValue> results, T user, float score)
        {
            if (user != null && !results.ContainsKey(user.Id))
                results.Add(user.Id, new TypeReaderValue(user, score));
        }
    }
}