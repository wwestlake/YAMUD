using System.ComponentModel.DataAnnotations;

namespace LagDaemon.YAMUD.Model.Map;

public class Exits
{
    [Key]
    public  Guid Id { get; set; }
    public Direction Direction { get; set; }
    public Guid ToRoom { get; set; }
}
