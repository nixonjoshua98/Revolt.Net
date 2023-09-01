using Revolt.Net.Commands._Original;
using Revolt.Net.Commands._Original.Builders;
using Revolt.Net.Commands._Original.Info;
using Revolt.Net.Commands.Context;

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