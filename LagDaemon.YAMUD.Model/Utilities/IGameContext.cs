using LagDaemon.YAMUD.Model.User;
using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public interface IGameContext
    {
        UserAccount CurrentUser { get; set; }
        bool BooleanResult { get; set; }
    }
}
