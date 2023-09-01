using Revolt.Net.Commands.Enums;
using System.Diagnostics;

namespace Revolt.Net.Commands._Original.Results
{
    [DebuggerDisplay(@"{DebuggerDisplay,nq}")]
    public class PreconditionGroupResult : PreconditionResult
    {
        public IReadOnlyCollection<PreconditionResult> PreconditionResults { get; }

        protected PreconditionGroupResult(CommandError? error, string errorReason, ICollection<PreconditionResult> preconditions)
            : base(error, errorReason)
        {
            PreconditionResults = (IReadOnlyCollection<PreconditionResult>)(preconditions ?? new List<PreconditionResult>(0).AsReadOnly());
        }

        public static new PreconditionGroupResult FromSuccess()
            => new(null, null, null);
        public static PreconditionGroupResult FromError(string reason, ICollection<PreconditionResult> preconditions)
            => new(CommandError.UnmetPrecondition, reason, preconditions);
        public static new PreconditionGroupResult FromError(Exception ex)
            => new(CommandError.Exception, ex.Message, null);
        public static new PreconditionGroupResult FromError(IResult result) //needed?
            => new(result.Error, result.ErrorReason, null);

        public override string ToString() => IsSuccess ? "Success" : $"{Error}: {ErrorReason}";
        private string DebuggerDisplay => IsSuccess ? "Success" : $"{Error}: {ErrorReason}";
    }
}
