using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Models;
using Revolt.Net.Commands.TypeBinding;
using Revolt.Net.Core.Hosting.Builders;
using System.Reflection;

namespace Revolt.Net.Commands.Builders
{
    public interface IRevoltCommandsBuilder
    {
        IRevoltCommandsBuilder AddModule<TModule>() where TModule : CommandModule;
        IRevoltCommandsBuilder AddModulesFromAssembly(Assembly assembly);
        IRevoltCommandsBuilder AddModulesFromAssemblyContaining<T>();
        IRevoltCommandsBuilder AddTypeParser<TType, TBinder>() where TBinder : class, ITypeParser<TType>;
        internal IRevoltCommandsBuilder AddTypeParser<TBinder>() where TBinder : class, ITypeParser;
    }

    internal sealed record RevoltCommandsBuilder(IRevoltBuilder Builder) : IRevoltCommandsBuilder
    {
        public IRevoltCommandsBuilder AddTypeParser<TType, TBinder>() where TBinder : class, ITypeParser<TType>
        {
            Builder.Services.AddSingleton<ITypeParser<TType>, TBinder>();

            return this;
        }

        public IRevoltCommandsBuilder AddTypeParser<TBinder>() where TBinder : class, ITypeParser
        {
            Builder.Services.AddSingleton<ITypeParser, TBinder>();

            return this;
        }

        public IRevoltCommandsBuilder AddModule<TModule>() where TModule : CommandModule
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b =>
            {
                b.AddModule<TModule>();
            });

            return this;
        }

        public IRevoltCommandsBuilder AddModulesFromAssembly(Assembly assembly)
        {
            Builder.Services.Configure<RevoltCommandStateBuilder>(b =>
            {
                b.AddModulesFromAssembly(assembly);
            });

            return this;
        }

        public IRevoltCommandsBuilder AddModulesFromAssemblyContaining<T>()
        {
            var assembly = typeof(T).Assembly;

            return AddModulesFromAssembly(assembly);
        }
    }
}
