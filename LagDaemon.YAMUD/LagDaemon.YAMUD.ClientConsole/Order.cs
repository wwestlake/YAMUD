using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.ClientConsole
{
    public enum CustomerType
    {
        Regular,
        Premium
    }

    public class Order
    {
        public decimal Total { get; set; }
        public CustomerType CustomerType { get; set; }
        public decimal Discount { get; private set; }

        public void ApplyDiscount(decimal discountAmount)
        {
            Discount = discountAmount;
        }
    }
}
