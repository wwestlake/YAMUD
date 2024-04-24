using LagDaemon.YAMUD.Model.Communication;
using Microsoft.AspNetCore.SignalR.Client;
namespace LagDaemon.YAMUD.WebClient.Services
{
    public class ChatService : IChatService
    {
        public event Action<RoomChatMessage> RoomMessageReceived;
        public event Action<GroupChatMessage> GroupMessageReceived;
        public event Action<NotificationMessage> NotificationReceived;

        private HubConnection _connection;
        private string _accessToken;
        private string _url;

        public async Task ConnectAsync(string accessToken, string url = "http://localhost:5180/chatHub")
        {
            _accessToken = accessToken;
            _url = url;
            _connection = new HubConnectionBuilder()
                .WithUrl(url, options => { 
                    options.AccessTokenProvider = () => {
                        Console.WriteLine($"Returning access token: {accessToken}");
                        return Task.FromResult(accessToken); 
                    };
                }).Build();

            _connection.On<RoomChatMessage>("ReceiveRoomMessage", message =>
            {
                RoomMessageReceived?.Invoke(message);
            });

            _connection.On<GroupChatMessage>("ReceiveGroupMessage", message =>
            {
                GroupMessageReceived?.Invoke(message);
            });

            _connection.On<NotificationMessage>("ReceiveNotification", message =>
            {
                NotificationReceived?.Invoke(message);
            });

            await _connection.StartAsync();
        }

        public async Task SendRoomChatMessageAsync(RoomChatMessage message)
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await ConnectAsync(_accessToken, _url);
            }

            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("SendRoomMessage", message);
            }
        }

        public async Task SendGroupChatMessageAsync(GroupChatMessage message)
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await ConnectAsync(_accessToken, _url);
            }
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.SendAsync("SendGroupMessage", message);
            }
        }

    }
}
