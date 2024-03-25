using FluentResults;
using LagDaemon.YAMUD.Model.Config;
using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.Tools
{
    public class CommunicationClient
    {
        private readonly HttpClient _httpClient;
        readonly HttpClientConfiguration _config;

        public CommunicationClient(HttpClient httpClient, HttpClientConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
        }

    }
}
