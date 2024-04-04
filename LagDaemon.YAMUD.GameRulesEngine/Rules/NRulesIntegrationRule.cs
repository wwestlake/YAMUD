using LagDaemon.YAMUD.Model.Utilities;
using NRules;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
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
        public new IRule AddRule(IRule rule)
        {
            throw new ApplicationException("Do not add rules to this rule, they would never be run");
        }

    }
}
