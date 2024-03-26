using LagDaemon.YAMUD.Model.Items;
using LagDaemon.YAMUD.Model.Map;
using System.Text.Json.Serialization;

namespace LagDaemon.YAMUD.Model.User;

public class PlayerState
{
    public PlayerState()
    {
        Id = Guid.NewGuid();
        Items = new List<ItemBase> { };
        CurrentLocation = new RoomAddress();
    }
    public Guid Id { get; set; }

    public bool IsAuthenticated { get; set; }
    public RoomAddress CurrentLocation { get; set; }

    public List<ItemBase> Items { get; set; }

    // Foreign key property
    public Guid UserAccountId { get; set; }

    [JsonIgnore]
    public UserAccount UserAccount { get; set; }
}
