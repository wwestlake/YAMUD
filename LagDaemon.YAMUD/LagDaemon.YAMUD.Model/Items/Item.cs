using System.ComponentModel.DataAnnotations;

namespace LagDaemon.YAMUD.Model.Items;


public abstract class Item
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
}

public class Money  : Item
{
    public int Amount { get; set; }
}


