using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Checks;
using Revolt.Net.Commands.Parsers;

namespace Revolt.Net.Commands.Builders
{
    /// <summary>
    /// Provides a fluent API for configuring and registering Revolt command modules, type parsers, and check handlers.
    /// </summary>
    public interface IRevoltCommandsBuilder
    {
        /// <summary>
        /// Adds a command module of the specified type.
        /// </summary>
        /// <typeparam name="TModule">The module class to add. Must inherit from <see cref="RevoltCommandModule"/>.</typeparam>
        /// <returns>The same builder instance for chaining.</returns>
        IRevoltCommandsBuilder AddModule<TModule>() where TModule : RevoltCommandModule;

        /// <summary>
        /// Adds all command modules found in the assembly containing the specified type.
        /// </summary>
        /// <typeparam name="T">A type contained in the target assembly.</typeparam>
        /// <returns>The same builder instance for chaining.</returns>
        IRevoltCommandsBuilder AddModulesFromAssemblyContaining<T>();

        /// <summary>
        /// Adds all pre-check handlers found in the assembly containing the specified type.
        /// </summary>
        /// <typeparam name="T">A type contained in the target assembly.</typeparam>
        /// <returns>The same builder instance for chaining.</returns>
        IRevoltCommandsBuilder AddCheckHandlersFromAssemblyContaining<T>();

        /// <summary>
        /// Adds a specific command pre-check and its corresponding handler.
        /// </summary>
        /// <typeparam name="TCheck">The check type implementing <see cref="ICommandPreCheck"/>.</typeparam>
        /// <typeparam name="TCheckHandler">The handler type implementing <see cref="ICommandPreCheckHandler{TCheck}"/>.</typeparam>
        /// <returns>The same builder instance for chaining.</returns>
        IRevoltCommandsBuilder AddCheckHandler<TCheck, TCheckHandler>()
            where TCheck : ICommandPreCheck
            where TCheckHandler : ICommandPreCheckHandler<TCheck>;

        /// <summary>
        /// Adds a type parser for a specific type.
        /// </summary>
        /// <typeparam name="TType">The target type the parser will convert to.</typeparam>
        /// <typeparam name="TBinder">The parser implementation, must implement <see cref="ITypeParser{TType}"/>.</typeparam>
        /// <returns>The same builder instance for chaining.</returns>
        IRevoltCommandsBuilder AddTypeParser<TType, TBinder>()
            where TBinder : class, ITypeParser<TType>;

        /// <summary>
        /// Adds a type parser that is not tied to a specific type.
        /// Intended for internal use where generic type inference is not required.
        /// </summary>
        /// <typeparam name="TBinder">The parser implementation, must implement <see cref="ITypeLessParser"/>.</typeparam>
        /// <returns>The same builder instance for chaining.</returns>
        internal IRevoltCommandsBuilder AddTypeParser<TBinder>()
            where TBinder : class, ITypeLessParser;

        /// <summary>
        /// Adds all type parsers found in the assembly containing the specified type.
        /// </summary>
        /// <typeparam name="T">A type contained in the target assembly. Must be a class.</typeparam>
        /// <returns>The same builder instance for chaining.</returns>
        IRevoltCommandsBuilder AddTypeParsersFromAssemblyContaining<T>()
            where T : class;
    }

}
