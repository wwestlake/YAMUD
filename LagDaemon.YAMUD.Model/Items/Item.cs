using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LagDaemon.YAMUD.Model.Items;


[Table("Items")]
public class Item
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime LastModifiedAt { get; set; }

    [Required]
    public long WearAndTear { get; set; }
    public long Weight { get; set; }

}
