using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public class NotRule : RuleBase
    {
        public NotRule(IRule subRule)
        {
            base.AddRule(subRule);
        }

        public new void AddRule(IRule rule)
        {
            throw new ApplicationException("Do not add rules to NotRule");
        }

        public override bool IsSatisfied(IGameContext context)
        {
            return !_rules[0].IsSatisfied(context);
        }
    }
}
