namespace LagDaemon.YAMUD.Model.Communication
{
    public enum Urgency {         
        Low,
        Medium,
        High
    }


    public class NotificationMessage : ChatMessageBase
    {
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public string LinkDescription { get; set; }
        public string Link { get; set; }

        public Urgency Urgency { get; set; } = Urgency.Low;
    }
}
