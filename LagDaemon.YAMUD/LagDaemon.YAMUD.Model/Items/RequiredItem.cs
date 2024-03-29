using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LagDaemon.YAMUD.Model.Items
{
    public class RequiredItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Item Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public CraftingRecipe CraftingRecipe { get; set; }
    }
}
