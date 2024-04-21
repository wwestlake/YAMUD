using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.GameCommands
{
    public enum Direction
    {
        North,
        NorthEast,
        NorthWest,
        South,
        SouthEast,
        SouthWest,
        East,
        West,
        Up,
        Down
    }

    public enum CommandToken
    {
        Go,
        Look,
        Examine,
        Take,
        Drop,
        Inventory,
        Inv,
        Use,
        Give,
        Equip,
        Unequip,
        Say,
        Tell,
        Emote,
        Whisper,
        Shout,
        Reply,
        TalkTo,
        Attack,
        Score,
        Help,
        Who,
        Where,
        Time,
        Kick,
        Ban,
        Mute,
        Set,
        Jail,
        Alias,
        Unalias,
        Recall,
        Scan,
        Info,
        Pose,
        Rp,
        WhisperIC,
        Hug,
        Kiss,
        Wave,
        Dance,
        Explore,
        Dig,
        Survey,
        Custom,
        Macro,
        Debug,
        Spawn,
        Teleport,
        ERROR,
        Exit,
        Message
    }
}
