using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Checks;
using Revolt.Net.Commands.Models;
using Revolt.Net.Commands.TypeBinding;
using Revolt.Net.Core.Builders;
using System.Reflection;

namespace Revolt.Net.Commands.Builders
{
    public interface IRevoltCommandsBuilder
    {
        IRevoltCommandsBuilder AddModule<TModule>() where TModule : CommandModule;
        IRevoltCommandsBuilder AddModulesFromAssemblyContaining<T>();
        IRevoltCommandsBuilder AddCheckHandlersFromAssemblyContaining<T>();
        IRevoltCommandsBuilder AddCheckHandler<TCheck, TCheckHandler>() where TCheck : ICommandPreCheck where TCheckHandler : ICommandPreCheckHandler<TCheck>;
        IRevoltCommandsBuilder AddTypeParser<TType, TBinder>() where TBinder : class, ITypeParser<TType>;
        internal IRevoltCommandsBuilder AddTypeParser<TBinder>() where TBinder : class, ITypeLessParser;
        IRevoltCommandsBuilder AddTypeParsersFromAssemblyContaining<T>() where T : class;
    }

    internal sealed record RevoltCommandsBuilder(IRevoltBuilder Builder) : IRevoltCommandsBuilder
    {
        public IRevoltCommandsBuilder AddCheckHandlersFromAssemblyContaining<T>()
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b => b.AddPreChecksFromAssembly(typeof(T).Assembly));

            return this;
        }

        public IRevoltCommandsBuilder AddCheckHandler<TCheck, TCheckHandler>()
            where TCheck : ICommandPreCheck
            where TCheckHandler : ICommandPreCheckHandler<TCheck>
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b => b.AddPreCheck<TCheck, TCheckHandler>());

            return this;
        }

        public IRevoltCommandsBuilder AddTypeParser<TType, TBinder>() 
            where TBinder : class, ITypeParser<TType>
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b => b.AddTypeParser<TType, TBinder>());

            return this;
        }

        public IRevoltCommandsBuilder AddTypeParser<TBinder>() 
            where TBinder : class, ITypeLessParser
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b => b.AddTypeParser<TBinder>());

            return this;
        }

        public IRevoltCommandsBuilder AddTypeParsersFromAssemblyContaining<T>()
            where T : class
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b => b.AddTypeParsersFromAssembly(typeof(T).Assembly));

            return this;
        }

        public IRevoltCommandsBuilder AddModule<TModule>() where TModule : CommandModule
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b => b.AddModule<TModule>());

            return this;
        }

        public IRevoltCommandsBuilder AddModulesFromAssemblyContaining<T>()
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b => b.AddModulesFromAssembly(typeof(T).Assembly));

            return this;
        }
    }
}
