using NRules.Fluent.Dsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.ClientConsole
{
    public class DiscountRule : Rule
    {
        public override void Define()
        {
            Order order = null;

            When()
                .Match<Order>(() => order, o => o.Total >= 100 && o.CustomerType == CustomerType.Premium);

            Then()
                .Do(ctx => order.ApplyDiscount(10));
        }
    }
}
