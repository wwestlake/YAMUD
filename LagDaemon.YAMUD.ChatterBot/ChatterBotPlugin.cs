using LagDaemon.YAMUD.Automation;
using LagDaemon.YAMUD.Model.Communication;
using LagDaemon.YAMUD.WebAPI.Services.ChatServices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace LagDaemon.YAMUD.ChatterBot
{
    public class ChatterBotPlugin : IPlugin
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly Random _random;
        private System.Timers.Timer _timer;

        public ChatterBotPlugin(IHubContext<ChatHub> hubContext, IConfiguration configuration)
        {
            _hubContext = hubContext;
            _configuration = configuration;
            _random = new Random();
        }

        public void Initialize()
        {
            // Start the timer when the plugin is initialized
            StartTimer();
        }

        private void StartTimer()
        {
            // Retrieve configuration values
            var minIntervalMinutes = _configuration.GetValue<int>("ChatterBot:MinIntervalMinutes");
            var maxIntervalMinutes = _configuration.GetValue<int>("ChatterBot:MaxIntervalMinutes");

            // Generate a random initial delay
            var initialDelayMilliseconds = _random.Next(minIntervalMinutes, maxIntervalMinutes) * 60000; // Convert minutes to milliseconds
            _timer = new System.Timers.Timer(initialDelayMilliseconds);
            _timer.Elapsed += async (_, _) => await SendMessage();
            _timer.Start();
        }

        private async Task SendMessage()
        {
            // Retrieve configuration values
            var messages = _configuration.GetSection("ChatterBot:Messages").Get<string[]>();

            // Select a random message and send it
            var randomMessage = messages[_random.Next(messages.Length)];

            // Send the message via ChatHub
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", new NotificationMessage { Message = randomMessage });

            // Restart the timer for the next message
            var minIntervalMinutes = _configuration.GetValue<int>("ChatterBot:MinIntervalMinutes");
            var maxIntervalMinutes = _configuration.GetValue<int>("ChatterBot:MaxIntervalMinutes");
            var intervalMilliseconds = _random.Next(minIntervalMinutes, maxIntervalMinutes) * 60000; // Convert minutes to milliseconds
            _timer.Interval = intervalMilliseconds;
        }
    }
}
