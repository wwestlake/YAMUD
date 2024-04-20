using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.GameCommands
{

    public class Command
    {
        public CommandToken Type { get; set; }
        public List<string> Parameters { get; set; } = [];

        public virtual bool Validate(IGameContext context)
        {
            return true;
        }

        public virtual string Execute(IGameContext context)
        {
            return "Command Executed";
        }
    }
}
