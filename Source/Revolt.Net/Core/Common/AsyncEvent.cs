namespace Revolt.Net.Core.Common
{
    public sealed class AsyncEvent<T>
    {
        private readonly List<Func<T, Task>> Listeners = new();

        public void Add(Func<T, Task> subscriber)
        {
            Listeners.Add(subscriber);
        }

        public void Remove(Func<T, Task> subscriber)
        {
            Listeners.Remove(subscriber);
        }

        internal async Task InvokeAsync(T @event)
        {
            try
            {
                await Task.WhenAll(Listeners.Select(ele => ele(@event)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
