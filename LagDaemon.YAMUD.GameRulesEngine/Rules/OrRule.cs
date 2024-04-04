using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public class OrRule : IRule
    {
        private List<IRule> _rules = new List<IRule>();

        public OrRule()
        {
        }

        public void AddRule(IRule rule)
        {
            _rules.Add(rule);
        }

        public bool IsSatisfied(IGameContext context)
        {
            return _rules.Any(x => x.IsSatisfied(context));
        }


    }
}
