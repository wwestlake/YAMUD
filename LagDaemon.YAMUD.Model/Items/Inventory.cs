using System.ComponentModel.DataAnnotations;

namespace LagDaemon.YAMUD.Model.Items
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "Inventory";

        [MaxLength(500)]
        public string Description { get; set; } = "Inventory";

        public Guid ParentId { get; set; }

        public ICollection<ItemInstance> Items { get; set; }
    }
}
