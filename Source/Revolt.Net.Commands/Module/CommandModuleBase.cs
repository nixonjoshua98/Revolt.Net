using Revolt.Net.Commands.Builders;
using Revolt.Net.Commands.Context;
using Revolt.Net.Commands.Info;

namespace Revolt.Net.Commands.Module
{
    /// <summary>
    /// Provides a base class for a command module to inherit from.
    /// </summary>
    public abstract class CommandModuleBase : CommandModuleBase<ICommandContext>
    {

    }

    /// <summary>
    /// Provides a base class for a command module to inherit from.
    /// </summary>
    /// <typeparam name="T">A class that implements <see cref="ICommandContext"/>.</typeparam>
    public abstract class CommandModuleBase<T> : ICommandModuleBase
        where T : class, ICommandContext
    {
        /// <summary>
        /// The underlying context of the command.
        /// </summary>
        /// <seealso cref="T:Revolt.Commands.ICommandContext" />
        /// <seealso cref="T:Revolt.Commands.CommandContext" />
        public T Context { get; private set; }

        /// <summary>
        ///     The method to execute before executing the command.
        /// </summary>
        /// <param name="command">The <see cref="CommandInfo"/> of the command to be executed.</param>
        protected virtual void BeforeExecute(CommandInfo command)
        {

        }

        /// <summary>
        ///  The method to execute after executing the command.
        /// </summary>
        /// <param name="command">The <see cref="CommandInfo"/> of the command to be executed.</param>
        protected virtual void AfterExecute(CommandInfo command)
        {

        }

        /// <summary>
        /// The method to execute when building the module.
        /// </summary>
        /// <param name="commandService">The <see cref="CommandService"/> used to create the module.</param>
        /// <param name="builder">The builder used to build the module.</param>
        protected virtual void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {

        }

        void ICommandModuleBase.SetContext(ICommandContext context)
        {
            Context = context as T ??
                throw new InvalidOperationException($"Invalid context type. Expected {typeof(T).Name}, got {context.GetType().Name}.");
        }

        void ICommandModuleBase.BeforeExecute(CommandInfo command) => BeforeExecute(command);

        void ICommandModuleBase.AfterExecute(CommandInfo command) => AfterExecute(command);

        void ICommandModuleBase.OnModuleBuilding(CommandService commandService, ModuleBuilder builder) => OnModuleBuilding(commandService, builder);
    }
}
