using Revolt.Net.Commands.Builders;
using Revolt.Net.Commands.Info;

namespace Revolt.Net.Commands.Module
{
    internal interface ICommandModuleBase
    {
        void SetContext(ICommandContext context);

        void BeforeExecute(CommandInfo command);

        void AfterExecute(CommandInfo command);

        void OnModuleBuilding(CommandService commandService, ModuleBuilder builder);
    }
}
