namespace LagDaemon.YAMUD.Model.Map;

public class Room
{
    public Guid Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, (int x, int y, int z)> Exits { get; set; }
}
