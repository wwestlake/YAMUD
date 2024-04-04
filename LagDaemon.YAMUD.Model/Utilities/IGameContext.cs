using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.Model.Items;
using LagDaemon.YAMUD.Model.User;
using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.Utilities;

public interface IGameContext
{
    UserAccount CurrentUser { get; set; }
    bool BooleanResult { get; set; }

    Character Actor { get; set; }
    Character Target { get; set; }

    Item InvolvedItem { get; set; }

    ActionType ActionType { get; set; }
}

public enum ActionType
{
    Attack,
    Defend,
    UseItem,
    GiveItem,
    TakeItem,

}
