using LagDaemon.YAMUD.Model.Communication;
using Microsoft.AspNetCore.SignalR;

namespace LagDaemon.YAMUD.WebAPI.Services.ChatService
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(NotificationMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
