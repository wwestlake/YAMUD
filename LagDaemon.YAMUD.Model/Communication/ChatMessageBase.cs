using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Communication
{
    public abstract class ChatMessageBase
    {
        public ChatMessageBase()
        {
            SentAt = DateTime.Now;
        }

        public string DisplayName { get; set; }
        public Guid To { get; set; }  // Represents the recipient (room, user, or group)
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public Guid From { get; set; }  // Represents the sender (user or system)
    }
}
