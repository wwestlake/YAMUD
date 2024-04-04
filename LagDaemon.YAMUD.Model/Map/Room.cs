namespace LagDaemon.YAMUD.Model.Map;

public class Room
{
    public Guid Id { get; set; }
    public Guid Owner { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public RoomAddress Address { get; set; }
    public Exits Exits { get; set; }
}
