using LagDaemon.YAMUD.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public class GameContext : IGameContext
    {
        public required UserAccount CurrentUser { get; set; }
        public bool BooleanResult { get; set; }
    }
}
