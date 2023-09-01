namespace Revolt.Net.Commands._Original
{
    internal class EmptyServiceProvider : IServiceProvider
    {
        public static readonly EmptyServiceProvider Instance = new();

        public object GetService(Type serviceType) => null;
    }
}
