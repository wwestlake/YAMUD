using System.ComponentModel.DataAnnotations;

namespace LagDaemon.YAMUD.Model.Items;


public class Item
{
    [Key]
    public int Id { get; set; }

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

public class Inventory
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public Guid ParentId { get; set; }

    public ICollection<Item> Items { get; set; }
}
