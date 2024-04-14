using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Automation
{
    public interface ICharacterBehaviorPlugin : IPlugin
    {
        void OnCharacterAction(Character character, ActionType action);
        // Other methods related to character behavior
    }
}
