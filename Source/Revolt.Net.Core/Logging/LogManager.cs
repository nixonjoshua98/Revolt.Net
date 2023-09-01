using Revolt.Net.Commands._Original;
using Revolt.Net.Common;

namespace Revolt.Net.Logging
{
    internal class LogManager
    {
        public LogSeverity Level { get; }

        private readonly AsyncEvent<Func<LogMessage, Task>> _Message = new();

        public event Func<LogMessage, Task> Message { add => _Message.Add(value); remove => _Message.Remove(value); }

        public LogManager(LogSeverity minSeverity)
        {
            Level = minSeverity;
        }

        public async Task LogAsync(LogSeverity severity, string source, Exception ex)
        {
            try
            {
                if (severity <= Level)
                    await _Message.InvokeAsync(new LogMessage(severity, source, null, ex)).ConfigureAwait(false);
            }
            catch
            {
                // ignored
            }
        }

        public async Task LogAsync(LogSeverity severity, string source, string message, Exception ex = null)
        {
            try
            {
                if (severity <= Level)
                    await _Message.InvokeAsync(new LogMessage(severity, source, message, ex)).ConfigureAwait(false);
            }
            catch
            {
                // ignored
            }
        }

        public async Task LogAsync(LogSeverity severity, string source, FormattableString message, Exception ex = null)
        {
            try
            {
                if (severity <= Level)
                    await _Message.InvokeAsync(new LogMessage(severity, source, message.ToString(), ex)).ConfigureAwait(false);
            }
            catch { }
        }


        public Task ErrorAsync(string source, Exception ex)
            => LogAsync(LogSeverity.Error, source, ex);
        public Task ErrorAsync(string source, string message, Exception ex = null)
            => LogAsync(LogSeverity.Error, source, message, ex);

        public Task ErrorAsync(string source, FormattableString message, Exception ex = null)
            => LogAsync(LogSeverity.Error, source, message, ex);


        public Task WarningAsync(string source, Exception ex)
            => LogAsync(LogSeverity.Warning, source, ex);
        public Task WarningAsync(string source, string message, Exception ex = null)
            => LogAsync(LogSeverity.Warning, source, message, ex);

        public Task WarningAsync(string source, FormattableString message, Exception ex = null)
            => LogAsync(LogSeverity.Warning, source, message, ex);


        public Task InfoAsync(string source, Exception ex)
            => LogAsync(LogSeverity.Info, source, ex);
        public Task InfoAsync(string source, string message, Exception ex = null)
            => LogAsync(LogSeverity.Info, source, message, ex);
        public Task InfoAsync(string source, FormattableString message, Exception ex = null)
            => LogAsync(LogSeverity.Info, source, message, ex);


        public Task VerboseAsync(string source, Exception ex)
            => LogAsync(LogSeverity.Verbose, source, ex);
        public Task VerboseAsync(string source, string message, Exception ex = null)
            => LogAsync(LogSeverity.Verbose, source, message, ex);
        public Task VerboseAsync(string source, FormattableString message, Exception ex = null)
            => LogAsync(LogSeverity.Verbose, source, message, ex);


        public Task DebugAsync(string source, Exception ex)
            => LogAsync(LogSeverity.Debug, source, ex);
        public Task DebugAsync(string source, string message, Exception ex = null)
            => LogAsync(LogSeverity.Debug, source, message, ex);
        public Task DebugAsync(string source, FormattableString message, Exception ex = null)
            => LogAsync(LogSeverity.Debug, source, message, ex);

        public Logger CreateLogger(string name) => new(this, name);
    }
}