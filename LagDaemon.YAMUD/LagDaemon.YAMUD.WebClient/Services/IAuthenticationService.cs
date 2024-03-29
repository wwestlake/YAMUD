using LagDaemon.YAMUD.WebClient.Model;

namespace LagDaemon.YAMUD.WebClient.Services
{
    // IAuthenticationService.cs
    public interface IAuthenticationService
    {
        AuthToken AuthToken { get; set; }
        Task<bool> IsUserAuthenticatedAsync();
    }
}
