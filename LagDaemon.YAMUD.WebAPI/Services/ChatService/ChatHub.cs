using LagDaemon.YAMUD.Model.Communication;
using Microsoft.AspNetCore.SignalR;

namespace LagDaemon.YAMUD.WebAPI.Services.ChatService
{
    public class ChatHub : Hub
    {
        public async Task SendRoomMessage(RoomChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveRoomMessage", message);
        }

        public async Task SendGroupMessage(GroupChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveGroupMessage", message);
        }


        public override async Task OnConnectedAsync()
        {
            // Send a welcome message to the connected client
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "Welcome to the chat!");

            // Call the base method to ensure proper hub initialization
            await base.OnConnectedAsync();
        }
    }
}
