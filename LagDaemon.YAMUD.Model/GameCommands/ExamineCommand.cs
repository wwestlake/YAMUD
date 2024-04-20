using LagDaemon.YAMUD.Model.Utilities;
using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.GameCommands
{
    public class ExamineCommand : Command
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
