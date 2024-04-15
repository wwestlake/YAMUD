namespace LagDaemon.YAMUD.Model.Communication
{
    public class NotificationMessage : ChatMessageBase
    {
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
