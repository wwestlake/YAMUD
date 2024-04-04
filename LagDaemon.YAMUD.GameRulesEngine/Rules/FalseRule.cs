using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public class FalseRule : RuleBase
    {
        public override bool IsSatisfied(IGameContext context)
        {
            return false;
        }
    }
}
