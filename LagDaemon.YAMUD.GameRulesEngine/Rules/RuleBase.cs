using LagDaemon.YAMUD.Model.Utilities;
using NRules;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public abstract class RuleBase : IRule
    {
        protected List<IRule> _rules = new List<IRule>();

        public abstract bool IsSatisfied(IGameContext context);


        public IRule AddRule(IRule rule)
        {
            _rules.Add(rule);
            return this;
        }
    }
}
