using LagDaemon.YAMUD.Model.Items;
using LagDaemon.YAMUD.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.User
{
    public class PlayerState
    {
        public PlayerState()
        {
            Items = new List<ItemBase> { };
            CurrentLocation = new RoomAddress();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public bool IsAuthenticated { get; set; }
        public RoomAddress CurrentLocation { get; set; }

        public List<ItemBase> Items { get; set; }

    }
}
