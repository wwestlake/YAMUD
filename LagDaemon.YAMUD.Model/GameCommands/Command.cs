using LagDaemon.YAMUD.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.GameCommands
{

    public class Command
    {
        public CommandToken Type { get; set; }
        public List<string> Parameters { get; set; }

        public virtual string Execute(IGameContext context)
        {
            return "Command Executed";
        }
    }
  
    public class GoCommand : Command
    {
        public Direction Direction { get; set; }

        public override string Execute(IGameContext context)
        {
            return "Go Command Executed";
        }
    }


}
