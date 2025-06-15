namespace Revolt.Net.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CommandAttribute(string name, int priority = 0) : Attribute
    {
        public readonly string Name = name;
        public readonly int Priority = priority;
    }
}
