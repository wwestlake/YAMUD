using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.Model.Items;
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
        public Character Actor { get; set; }
        public Character Target { get; set; }
        public Item InvolvedItem { get; set; }
        public ActionType ActionType { get; set; }
    }
}
