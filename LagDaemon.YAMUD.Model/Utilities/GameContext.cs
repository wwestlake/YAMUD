using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.Model.Items;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public class GameContext : IGameContext
    {
        public required UserAccount CurrentUser { get; set; }
        public bool BooleanResult { get; set; }
        public required Character Actor { get; set; }
        public Character? Target { get; set; }
        public Item? InvolvedItem { get; set; }
        public ActionType ActionType { get; set; }
        public IEnumerable<Room> Map { get; set; } = new List<Room>();
    }
}
