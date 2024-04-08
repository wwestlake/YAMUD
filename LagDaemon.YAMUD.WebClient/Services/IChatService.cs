using LagDaemon.YAMUD.Model.Communication;
namespace LagDaemon.YAMUD.WebClient.Services
{
    public interface IChatService
    {
        Task ConnectAsync(string url = "http://localhost:5180/chatHub");
        Task SendRoomChatMessageAsync(RoomChatMessage message);
    }
}
