using LagDaemon.YAMUD.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.GameRulesEngine.Rules
{
    public interface IRule
    {
        bool IsSatisfied(IGameContext context);
    }
}
