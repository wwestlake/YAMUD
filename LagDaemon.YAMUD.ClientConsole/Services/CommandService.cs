using LagDaemon.YAMUD.ClientConsole.UserAccount;
using LagDaemon.YAMUD.Model.GameCommands;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Retry;

namespace LagDaemon.YAMUD.ClientConsole.Services
{
    public class CommandService
    {
        public event Action<Command> CommandReceived;

        private string? _commandHubUrl;
        private AccountManager _accountManager;
        private HubConnection _connection;

        public CommandService(IConfiguration config, AccountManager accountManager)
        {
            _commandHubUrl = config["CommandHubUrl"];
            _accountManager = accountManager;
            ConnectAsync().Wait();
        }

        public async Task ConnectAsync()
        {
            RetryStrategyOptions options = new RetryStrategyOptions()
            {
                OnRetry = static args =>
                {
                    Console.WriteLine($"OnRetry, Attempt: {args.AttemptNumber}, {args.Duration}");
                    return default;
                }
            };


            ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
                 .AddRetry(new Polly.Retry.RetryStrategyOptions())
                 .AddTimeout(TimeSpan.FromSeconds(10))
                 .Build();

            await pipeline.ExecuteAsync(async token => { await _ConnectAsync(); });
        }

        private async Task<string> _ConnectAsync()
        {
            if (_commandHubUrl == null)
            {
                return "No CommandHubUrl configured.";
            }
            _connection = new HubConnectionBuilder()
                  .WithUrl(_commandHubUrl, options => {
                      options.AccessTokenProvider = () => Task.FromResult(_accountManager.Token);
                  }).Build();

            _connection.On<MessageCommand>("ReceiveCommand", command =>
            {
                CommandReceived?.Invoke(command);
            });

            await _connection.StartAsync();

            return string.Empty;
        }

        public async Task SendCommand(Command command)
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await ConnectAsync();
            }

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
}
