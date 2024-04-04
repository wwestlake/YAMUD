using LagDaemon.YAMUD.Model.Utilities;
using System.Reflection.Metadata.Ecma335;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public class OrRule : RuleBase
    {
        public override bool IsSatisfied(IGameContext context)
        {
            return _rules.Any(x => x.IsSatisfied(context));
        }
    }
}
