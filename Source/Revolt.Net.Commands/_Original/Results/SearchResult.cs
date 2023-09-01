using Revolt.Net.Commands.Enums;
using System.Diagnostics;

namespace Revolt.Net.Commands._Original.Results
{
    [DebuggerDisplay(@"{DebuggerDisplay,nq}")]
    public readonly struct SearchResult : IResult
    {
        public string Text { get; }
        public IReadOnlyList<CommandMatch> Commands { get; }

        /// <inheritdoc/>
        public CommandError? Error { get; }
        /// <inheritdoc/>
        public string ErrorReason { get; }

        /// <inheritdoc/>
        public bool IsSuccess => !Error.HasValue;

        private SearchResult(string text, IReadOnlyList<CommandMatch> commands, CommandError? error, string errorReason)
        {
            Text = text;
            Commands = commands;
            Error = error;
            ErrorReason = errorReason;
        }

        public static SearchResult FromSuccess(string text, IReadOnlyList<CommandMatch> commands)
            => new(text, commands, null, null);
        public static SearchResult FromError(CommandError error, string reason)
            => new(null, null, error, reason);
        public static SearchResult FromError(Exception ex)
            => FromError(CommandError.Exception, ex.Message);
        public static SearchResult FromError(IResult result)
            => new(null, null, result.Error, result.ErrorReason);

        public override string ToString() => IsSuccess ? "Success" : $"{Error}: {ErrorReason}";
        private string DebuggerDisplay => IsSuccess ? $"Success ({Commands.Count} Results)" : $"{Error}: {ErrorReason}";
    }
}
