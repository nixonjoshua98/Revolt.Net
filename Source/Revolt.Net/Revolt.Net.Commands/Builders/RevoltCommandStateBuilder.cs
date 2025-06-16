using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Checks;
using Revolt.Net.Commands.Models;
using Revolt.Net.Commands.Parsers;
using System.Reflection;

namespace Revolt.Net.Commands.Builders
{
    internal sealed class RevoltCommandStateBuilder
    {
        private readonly HashSet<Type> RegisteredModuleTypes = [];
        private readonly List<Type> TypeParsers = [];
        private readonly Dictionary<Type, Type> GenericTypeParsers = [];
        private readonly Dictionary<Type, Type> PreChecks = [];

        public void AddModule<T>() where T : RevoltCommandModule
        {
            AddModule(typeof(T));
        }

        public void AddModule(Type moduleType)
        {
            RegisteredModuleTypes.Add(moduleType);
        }

        public void AddTypeParser<TType, TBinder>() where TBinder : class, ITypeParser<TType>
        {
            GenericTypeParsers[typeof(TType)] = typeof(TBinder);
        }

        public void AddTypeParser(Type valueType, Type parserType)
        {
            GenericTypeParsers[valueType] = parserType;
        }

        public void AddTypeParser<TBinder>() where TBinder : class, ITypeLessParser
        {
            TypeParsers.Add(typeof(TBinder));
        }

        private void AddTypeParser(Type parserType)
        {
            TypeParsers.Add(parserType);
        }

        public void AddTypeParsersFromAssembly(Assembly assembly)
        {
            var typeParserType = typeof(ITypeLessParser);
            var genericTypeParserType = typeof(ITypeParser<>);

            var types = assembly.DefinedTypes
                .Where(t => !t.IsAbstract && !t.IsInterface);

            foreach (var type in types)
            {
                if (type.IsAssignableTo(typeParserType))
                {
                    AddTypeParser(type);
                }
                else
                {
                    var interfaces = type.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericTypeParserType);

                    foreach (var iface in interfaces)
                    {
                        var valueType = iface.GenericTypeArguments.Single();

                        AddTypeParser(valueType, type);
                    }
                }
            }
        }

        public void AddModulesFromAssembly(Assembly assembly)
        {
            var commandModuleType = typeof(RevoltCommandModule);

            var modules = assembly.DefinedTypes
                .Where(t => t.IsAssignableTo(commandModuleType));

            foreach (var module in modules)
            {
                AddModule(module);
            }
        }

        public void AddPreChecksFromAssembly(Assembly assembly)
        {
            var handlerInterfaceGeneric = typeof(ICommandPreCheckHandler<>);

            var handlers = assembly.DefinedTypes
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceGeneric)
                    .Select(i => new { HandlerType = t.AsType(), PreCheckType = i.GetGenericArguments()[0] }));

            foreach (var pair in handlers)
            {
                AddPreCheck(pair.PreCheckType, pair.HandlerType);
            }
        }

        public void AddPreCheck<TCheck, TCheckHandler>()
            where TCheck : ICommandPreCheck
            where TCheckHandler : ICommandPreCheckHandler<TCheck>
        {
            PreChecks[typeof(TCheck)] = typeof(TCheckHandler);
        }

        public void AddPreCheck(Type preCheck, Type preCheckHandler)
        {
            PreChecks[preCheck] = preCheckHandler;
        }

        public RevoltCommandsState Build()
        {
            return new RevoltCommandsState(
                RegisteredModuleTypes,
                TypeParsers,
                GenericTypeParsers,
                PreChecks
            );
        }
    }
}
