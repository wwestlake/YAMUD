using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.GameCommands
{

    public class Command
    {
        public CommandToken Type { get; set; }
        public List<string> Parameters { get; set; } = [];

        public Func<IGameContext, string> CommandAction { get; set; }
            = context => "|Command Executed|";

        public virtual bool Validate(IGameContext context)
        {
            return true;
        }
       
        public string Execute(IGameContext context)
        {
            return CommandAction?.Invoke(context);
        }

    }
}
