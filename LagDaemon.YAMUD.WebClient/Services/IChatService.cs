using LagDaemon.YAMUD.Model.Communication;
namespace LagDaemon.YAMUD.WebClient.Services
{
    public interface IChatService
    {
        event Action<RoomChatMessage> RoomMessageReceived;
        event Action<GroupChatMessage> GroupMessageReceived;


        Task ConnectAsync(string token, string url = "http://localhost:5180/chatHub");
        Task SendRoomChatMessageAsync(RoomChatMessage message);
        Task SendGroupChatMessageAsync(GroupChatMessage message);
    }
}
