using LagDaemon.YAMUD.Model.GameCommands;
using Microsoft.AspNetCore.SignalR;

namespace LagDaemon.YAMUD.WebAPI.Services.CommandServices
{
    public class CommandHub : Hub
    {
        public async Task SendCommand(Command command)
        {
            await Clients.All.SendAsync("ReceiveCommand", command);
        }

        public override async Task OnConnectedAsync()
        {
            // Send a welcome message to the connected client
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveCommand", new MessageCommand { Parameters = new List<string> { "Welcome to the CommandHub!" } });

            // Call the base method to ensure proper hub initialization
            await base.OnConnectedAsync();
        }
    }
}
