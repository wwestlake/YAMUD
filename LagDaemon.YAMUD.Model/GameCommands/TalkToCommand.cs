using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.GameCommands
{
    internal class TalkToCommand : Command
    {
        public override bool Validate(IGameContext context)
        {
            return true;
        }
        public override string Execute(IGameContext context)
        {
            return "Command Executed";
        }
    }
}