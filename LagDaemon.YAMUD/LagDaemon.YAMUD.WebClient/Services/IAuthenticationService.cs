using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebClient.Model;

namespace LagDaemon.YAMUD.WebClient.Services
{
    // IAuthenticationService.cs
    public interface IAuthenticationService
    {
        AuthToken AuthToken { get; set; }
        Task<bool> IsUserAuthenticatedAsync();

        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<UserAccount> GetUserAsync();

    }
}
