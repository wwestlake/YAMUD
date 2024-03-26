using LagDaemon.YAMUD.Model.Items;
using System.ComponentModel.DataAnnotations.Schema;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public class Inventory
    {
        public Inventory()
        {
            Items = new Dictionary<string, ItemBase>();
        }

        [Column(TypeName = "jsonb")]
        public IDictionary<string, ItemBase> Items { get; set; }
    }
}
