using System;
using System.Linq;

namespace LagDaemon.YAMUD.Model.Communication
{
    public class GroupChatMessage : ChatMessageBase
    {
        public string Group { get; set; }
        // Additional properties specific to group chat messages can be added here
    }
}
