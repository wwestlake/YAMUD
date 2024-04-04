using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebClient.Model;

namespace LagDaemon.YAMUD.WebClient.Services
{
    // IAuthenticationService.cs
    public interface IAuthenticationService
    {
        event EventHandler AuthenticationStateChanged;
        Authority Authority { get; }
        Task<bool> IsUserAuthenticatedAsync();
        Task<bool> LoginAsync(string username, string password);
        bool Authorize(string token);
        Task LogoutAsync();
        Task<UserAccount> GetUserAsync(bool useCache = true);
        Task<bool> RegisterAsync(string email, string password, string displayName);
    }
}
