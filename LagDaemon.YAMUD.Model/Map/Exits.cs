using LagDaemon.YAMUD.Model.GameCommands;
using System.ComponentModel.DataAnnotations;

namespace LagDaemon.YAMUD.Model.Map;

public class Exit
{
    [Key]
    public  Guid Id { get; set; }
    public Direction Direction { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ToRoom { get; set; }
}
