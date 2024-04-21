using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.GameCommands
{
    internal class TellCommand : Command
    {
        public override bool Validate(IGameContext context)
        {
            return true;
        }
    }
}