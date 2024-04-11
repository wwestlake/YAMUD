namespace LagDaemon.YAMUD.Model.Map;

public class Room
{
    public Room()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public required Guid Owner { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public required RoomAddress Address { get; set; }
    public ICollection<Exit> Exits { get; set; }
}
