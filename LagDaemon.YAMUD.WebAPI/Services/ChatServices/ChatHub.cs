using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.Communication;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Model.Utilities;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Core.Connections;
using System.IdentityModel.Tokens.Jwt;

namespace LagDaemon.YAMUD.WebAPI.Services.ChatServices
{
    public class ChatHub : Hub
    {
        private readonly IDataCacheService _dataCache;
        private readonly IRequestContext _requestContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Room> _roomRepo;
        private readonly IRepository<UserAccount> _userRepo;


        public ChatHub(IDataCacheService dataCache, IRequestContext requestContext, IUnitOfWork unitOfWork)
        {
            _dataCache = dataCache;
            _requestContext = requestContext;
            _unitOfWork = unitOfWork;
            _roomRepo = unitOfWork.GetRepository<Room>();
            _userRepo = unitOfWork.GetRepository<UserAccount>();
        }

        private (string, UserAccount, Room) FindRoom()
        {
            string email = GetUserFromToken().Id;
            UserAccount user = _userRepo.Get().FirstOrDefault(x => x.EmailAddress == email);
            var x = user.PlayerState.CurrentLocation.X;
            var y = user.PlayerState.CurrentLocation.Y;
            var level = user.PlayerState.CurrentLocation.Level;

            Room room = _roomRepo.Get().FirstOrDefault(
                r => r.Address.X == x && r.Address.Y == y && r.Address.Level == level
            );
            return (email, user, room);
        }

        public async Task SendRoomMessage(RoomChatMessage message)
        {
            var (email, user, room) = FindRoom();
            message.DisplayName = user.DisplayName;
            message.From = user.ID;
            message.To = room.Id;

            if (room != null)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveRoomMessage", message);
                await Clients.Groups(room.Name).SendAsync("ReceiveGroupMessage", message);
            }
        }

        public async Task SendGroupMessage(GroupChatMessage message)
        {
            await Clients.Groups(message.Group).SendAsync("ReceiveGroupMessage", message);
        }

        public async Task SendNotificationMessage(NotificationMessage message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var (_, _, room) = FindRoom();
            if (room == null)
            {
                throw new ApplicationException("Room cannot be null here");
            }

            if (room == null)
            {
                throw new ApplicationException("Room cannot be null here");
            }

            if (ConnectionMapping<string>.GetConnections(room.Name).Contains(connectionId))
            {
                await base.OnConnectedAsync();
                return;
            }

            await JoinGroup(room.Name);

            // Send a welcome message to the connected client
            await Clients.Group(room.Name).SendAsync("ReceiveRoomMessage", new RoomChatMessage
            {
                DisplayName = "System",
                From = Guid.Empty,
                To = room.Id,
                Message = $"Welcome to {room.Name}!"
            });

            await Clients.Client(connectionId).SendAsync("ReceiveNotification", new NotificationMessage
            {
                Subject = "Welcome!",
                Message = $"You have joined {room.Name}",
                Link = "/chat",
                LinkDescription = "Go to the chat page and see who's there!",
                Urgency = Urgency.Medium
            });

            // Call the base method to ensure proper hub initialization
            await base.OnConnectedAsync();
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            ConnectionMapping<string>.Add(groupName, Context.ConnectionId);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            ConnectionMapping<string>.Remove(groupName, Context.ConnectionId);
        }


        private UserInfo GetUserFromToken()
        {
            var token = Context.GetHttpContext().Request.Query["access_token"].FirstOrDefault();

            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Extract user information from the token
                var userIdString = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

                var userName = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;

                return new UserInfo { Id = userIdString, Name = userName };
            }
            else
            {
                return null;
            }
        }

    }


    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
