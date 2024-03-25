using LagDaemon.YAMUD.Model.Items;
using LagDaemon.YAMUD.Model.Map;

namespace LagDaemon.YAMUD.Model.User;

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

    public UserAccount UserAccount { get; set; }
}
