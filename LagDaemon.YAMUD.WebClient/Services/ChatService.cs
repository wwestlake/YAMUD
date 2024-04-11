using LagDaemon.YAMUD.Model.Communication;
using Microsoft.AspNetCore.SignalR.Client;
namespace LagDaemon.YAMUD.WebClient.Services
{
    public class ChatService : IChatService
    {
        public event Action<RoomChatMessage> RoomMessageReceived;
        public event Action<GroupChatMessage> GroupMessageReceived;

        private HubConnection _connection;
        public async Task ConnectAsync(string accessToken, string url = "http://localhost:5180/chatHub")
        {
            _connection = new HubConnectionBuilder()
                
                .WithUrl(url, options => { 
                    options.AccessTokenProvider = () => Task.FromResult(accessToken);
                }).Build();

            _connection.On<RoomChatMessage>("ReceiveRoomMessage", message =>
            {
                RoomMessageReceived?.Invoke(message);
            });

            _connection.On<GroupChatMessage>("ReceiveGroupMessage", message =>
            {
                GroupMessageReceived?.Invoke(message);
            });

            await _connection.StartAsync();
        }

        public async Task SendRoomChatMessageAsync(RoomChatMessage message)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("SendRoomMessage", message);
            }
        }

        public async Task SendGroupChatMessageAsync(GroupChatMessage message)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("SendGroupMessage", message);
            }
        }

    }
}
