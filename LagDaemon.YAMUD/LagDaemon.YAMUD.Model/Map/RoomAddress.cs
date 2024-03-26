namespace LagDaemon.YAMUD.Model.Map;

public class RoomAddress
{
    public RoomAddress()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Level { get; set; }
}
