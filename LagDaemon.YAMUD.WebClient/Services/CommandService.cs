using LagDaemon.YAMUD.Model.GameCommands;
using Microsoft.AspNetCore.SignalR.Client;

namespace LagDaemon.YAMUD.WebClient.Services;

public class CommandService
{
    public event Action<Command> CommandReceived;

    private string? _commandHubUrl;
    private HubConnection _connection;

    public CommandService(string url)
    {
        _commandHubUrl = url;
    }

    public async Task ConnectAsync(string accessToken)
    {
        await _ConnectAsync(accessToken);
    }

    private async Task<string> _ConnectAsync(string accessToken)
    {
        if (_commandHubUrl == null)
        {
            return "No CommandHubUrl configured.";
        }
        _connection = new HubConnectionBuilder()
              .WithUrl(_commandHubUrl, options => {
                  options.AccessTokenProvider = () => Task.FromResult(accessToken);
              }).Build();

        _connection.On<MessageCommand>("ReceiveCommand", command =>
        {
            CommandReceived?.Invoke(command);
        });

        _connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await ConnectAsync(accessToken);
        };

        await _connection.StartAsync();

        return string.Empty;
    }

    public async Task SendCommandAsync(IEnumerable<Command> commands)
    {
        foreach (var cmd in commands)
        {
            await SendCommandAsync(cmd);
        }
    }

    public async Task SendCommandAsync(Command command)
    {
        if (_connection.State == HubConnectionState.Connected)
        {
            try
            {
                await _connection.SendAsync("SendCommand", command);
            } catch (Exception ex)
            {
                Console.WriteLine($"Error sending command: {ex.Message}");
            }
        }
    }

}
