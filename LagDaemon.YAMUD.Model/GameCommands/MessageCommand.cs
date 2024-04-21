using LagDaemon.YAMUD.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.GameCommands
{
    public class MessageCommand : Command
    {
        public MessageCommand()
        {
            Type = CommandToken.Message;
        }

        public override bool Validate(IGameContext context)
        {
            return true;
        }

        
    }
}
