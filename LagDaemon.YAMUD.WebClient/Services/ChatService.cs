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
        private bool firstConnection = true;

        public ChatService()
        {
            Console.WriteLine("ChatService instance created");
        }

        public async Task ConnectAsync(string accessToken, string url = "http://localhost:5180/chatHub")
        {
            Console.WriteLine("Connecting to chat service...");
            _accessToken = accessToken;
            _url = url;
            _connection = new HubConnectionBuilder()
                .WithUrl(url, options => { 
                    options.AccessTokenProvider = () => {
                        Console.WriteLine($"Returning access token: {accessToken}");
                        return Task.FromResult(accessToken); 
                    };
                }).Build();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await ConnectAsync(_accessToken, _url);
            };

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

            if (firstConnection)
            {
                RoomMessageReceived += ChatService_RoomMessageReceived;
                NotificationReceived += ChatService_NotificationReceived;
                firstConnection = false;
            }
            await _connection.StartAsync();
        }

        private void ChatService_NotificationReceived(NotificationMessage message)
        {
            InMemoryStorageService.AddNotification(message);
        }

        private void ChatService_RoomMessageReceived(RoomChatMessage msg)
        {
            InMemoryStorageService.AddChatMessage(msg);
        }

        public async Task SendRoomChatMessageAsync(RoomChatMessage message)
        {
            
            await _connection.SendAsync("SendRoomMessage", message);
        }

        public async Task SendGroupChatMessageAsync(GroupChatMessage message)
        {
            await _connection.SendAsync("SendGroupMessage", message);
        }

    }
}
