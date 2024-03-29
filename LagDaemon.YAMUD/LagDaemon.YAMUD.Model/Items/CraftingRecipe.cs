using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Items
{
    public class CraftingRecipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Item OutputItem { get; set; }

        [Required]
        public ICollection<RequiredItem> RequiredItems { get; set; } = new List<RequiredItem>();
    }
}
