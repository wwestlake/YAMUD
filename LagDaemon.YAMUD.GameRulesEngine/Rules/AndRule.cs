using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{

    public class AndRule : RuleBase
    {
        public override bool IsSatisfied(IGameContext context)
        {
            return _rules.All(x => x.IsSatisfied(context));
        }
    }
}
