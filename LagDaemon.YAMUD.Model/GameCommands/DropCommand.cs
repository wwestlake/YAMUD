﻿using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.GameCommands
{
    internal class DropCommand : Command
    {
        public override bool Validate(IGameContext context)
        {
            return true;
        }
    }
}