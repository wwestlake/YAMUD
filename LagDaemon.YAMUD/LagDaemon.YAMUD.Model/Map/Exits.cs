using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Map
{
    public class Exits
    {
        [Key]
        public  Guid Id { get; set; }
        public Direction Direction { get; set; }
        public Guid ToRoom { get; set; }
    }
}
