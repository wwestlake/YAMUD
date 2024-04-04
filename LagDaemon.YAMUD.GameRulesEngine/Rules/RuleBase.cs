using LagDaemon.YAMUD.Model.Utilities;
using NRules;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public abstract class RuleBase : IRule
    {
        protected List<IRule> _rules = new List<IRule>();

        public abstract bool IsSatisfied(IGameContext context);


        public void AddRule(IRule rule)
        {
            _rules.Add(rule);
        }
    }

    public class FunctionRule : RuleBase
    {
        private Func<IGameContext, bool> _function;

        public FunctionRule(Func<IGameContext, bool> function)
        {
            _function = function;
        }

        public override bool IsSatisfied(IGameContext context)
        {
            return _function(context);
        }
    }

    public class NRulesIntegrationRule : RuleBase
    {
        private readonly ISessionFactory sessionFactory;

        public NRulesIntegrationRule(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public override bool IsSatisfied(IGameContext context)
        {
            var session = sessionFactory.CreateSession();
            // Insert facts (in this case, the game context)
            session.Insert(context);

            // Fire all rules
            session.Fire();

            // Return the game over status from the game context
            return context.BooleanResult;
        }
    }

}
