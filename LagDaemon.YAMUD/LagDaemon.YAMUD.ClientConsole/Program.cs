using LagDaemon.YAMUD.ClientConsole;
using NRules;
using NRules.Fluent;

var repository = new RuleRepository();
repository.Load(x => x.From(typeof(DiscountRule)));
var factory = repository.Compile();
var session = factory.CreateSession();

// Insert facts into the session (e.g., order)
var order = new Order { Total = 120, CustomerType = CustomerType.Premium };
session.Insert(order);

// Fire the rules
session.Fire();

// Output the result
Console.WriteLine($"Order Total: {order.Total}, Discount Applied: {order.Discount}");
