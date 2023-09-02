using Revolt.Net.Commands.Context;
using Revolt.Net.Commands.Results;
using System.Reflection;

namespace Revolt.Net.Commands.Readers
{
    internal static class NullableTypeReader
    {
        public static TypeReader Create(Type type, TypeReader reader)
        {
            var constructor = typeof(NullableTypeReader<>).MakeGenericType(type).GetTypeInfo().DeclaredConstructors.First();
            return (TypeReader)constructor.Invoke(new object[] { reader });
        }
    }

    internal class NullableTypeReader<T> : TypeReader
        where T : struct
    {
        private readonly TypeReader _baseTypeReader;

        public NullableTypeReader(TypeReader baseTypeReader)
        {
            _baseTypeReader = baseTypeReader;
        }

        /// <inheritdoc />
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            if (string.Equals(input, "null", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "nothing", StringComparison.OrdinalIgnoreCase))
                return TypeReaderResult.FromSuccess(new T?());
            return await _baseTypeReader.ReadAsync(context, input, services).ConfigureAwait(false);
        }
    }
}
