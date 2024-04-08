using LagDaemon.YAMUD.Model.Communication;
using Microsoft.AspNetCore.SignalR.Client;
namespace LagDaemon.YAMUD.WebClient.Services
{
    public class ChatService : IChatService
    {
        private HubConnection _connection;

        public ChatService()
        {
            ConnectAsync().Wait();
        }

        public async Task ConnectAsync(string url = "http://localhost:5180/chatHub")
        {
            _connection = new HubConnectionBuilder()
                
                .WithUrl(url)
                .Build();

            _connection.On<RoomChatMessage>("ReceiveMessage", message =>
            {
                // Handle incoming message
                Console.WriteLine($"Received message: {message.Message}");
            });

            await _connection.StartAsync();
        }

        public async Task SendRoomChatMessageAsync(RoomChatMessage message)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("SendMessage", message);
            }
        }
    }
}
