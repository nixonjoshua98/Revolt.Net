using System.Collections.Immutable;

namespace Revolt.Net.Core
{
    public class AsyncEvent<T> where T : class
    {
        private readonly Lock _subLock = new();

        internal ImmutableArray<Func<T, CancellationToken, Task>> _subscriptions = [];

        public IReadOnlyList<Func<T, CancellationToken, Task>> Subscriptions => _subscriptions;

        public void Add(Func<T, CancellationToken, Task> subscriber)
        {
            lock (_subLock)
            {
                _subscriptions = _subscriptions.Add(subscriber);
            }
        }

        public void Remove(Func<T, CancellationToken, Task> subscriber)
        {
            lock (_subLock)
            {
                _subscriptions = _subscriptions.Remove(subscriber);
            }
        }
    }

    internal static class EventExtensions
    {
        public static async Task InvokeAsync<T>(this AsyncEvent<T> eventHandler, T arg, CancellationToken cancellationToken) where T : class
        {
            var subscribers = eventHandler.Subscriptions;

            for (int i = 0; i < subscribers.Count; i++)
            {
                await subscribers[i].Invoke(arg, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}