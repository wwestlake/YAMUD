using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public class TrueRule : RuleBase
    {
        public override bool IsSatisfied(IGameContext context)
        {
            return true;
        }
    }
}
