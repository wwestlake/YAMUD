using LagDaemon.YAMUD.WebClient.Model;

namespace LagDaemon.YAMUD.WebClient.Services
{
    public class Authority
    {
        public AuthToken? AuthToken { get; set; }
        public string? DisplayName { get; set; }
        public string? Role { get; set; }
    }
}
