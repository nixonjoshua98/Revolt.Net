namespace Revolt.Net.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CommandAttribute(string name) : Attribute
    {
        public readonly string Name = name;
    }
}
