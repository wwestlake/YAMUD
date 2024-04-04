using NRules.Fluent.Dsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.ClientConsole
{
    public enum Role
    {
        Admin,
        Moderator,
        Player
    }

    public class DiscountRule : Rule
    {
        public override void Define()
        {
            Order order = null;
            Role role = Role.Player;

            When()
                .Match<Order>(() => order, o => o.Total >= 100 && o.CustomerType == CustomerType.Premium)
                .Match<Role>(() => role, r => r > Role.Moderator );

            Then()
                .Do(ctx => order.ApplyDiscount(10));
        }
    }
}
