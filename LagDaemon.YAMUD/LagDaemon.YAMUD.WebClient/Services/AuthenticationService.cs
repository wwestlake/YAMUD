using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebClient.Model;
using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace LagDaemon.YAMUD.WebClient.Services
{

    // AuthenticationService.cs
    public class AuthenticationService : IAuthenticationService
    {
        public event EventHandler  AuthenticationStateChanged;

        private AuthToken? _authToken;
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        public Authority Authority { get; private set; }

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, Authority authority)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            Authority = authority;
        }

        protected virtual void OnAuthenticationStateChanged()
        {
            AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool Authorize(string token)
        {
            try
            {
                _authToken = new AuthToken() { token = token };
                GetClaims(_authToken.token).Wait();
                Authority.AuthToken = _authToken;
                _localStorage.SetObjectAsync("authToken", _authToken.token);
                OnAuthenticationStateChanged(); 
                return true;
            } catch
            {
                OnAuthenticationStateChanged();
                return false;
            }

        }

        public async Task<UserAccount> GetUserAsync()
        {
            //GetCurrentUser

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7214/api/UserAccount/GetCurrentUser");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Authority.AuthToken.token);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<UserAccount>(await response.Content.ReadAsStringAsync());
                    
            } else {
                return null;
            }
        }

        // Implement authentication logic here
        public async Task<bool> IsUserAuthenticatedAsync()
        {
            // Check if the user is authenticated
            // For example, you can check if the user has a valid authentication token
            return await Task.FromResult(Authority.AuthToken != null); // Placeholder, replace with actual implementation
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var authenticationRequest = new
            {
                emailAddress = username,
                password = password
            };


            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7214/api/UserAccount/Authenticate");
            request.Content = new StringContent(JsonSerializer.Serialize(authenticationRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _authToken = JsonSerializer.Deserialize<AuthToken>(responseContent);

                try
                {
                    GetClaims(_authToken.token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }

                // Save token to local storage
                await _localStorage.SetObjectAsync("authToken", _authToken.token);

                Authority.AuthToken = _authToken;
                OnAuthenticationStateChanged();
                return true;
            }
            else
            {
                // Authentication failed, handle error
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            Authority.DisplayName = string.Empty;
            Authority.Role = string.Empty;
            Authority.AuthToken = null;
            OnAuthenticationStateChanged();
        }

        public async Task<bool> RegisterAsync(string displayName, string email, string password)
        {
            var registrationRequest = new
            {
                displayName = displayName,
                email = email,
                password = password
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7214/api/UserAccount/CreateAccount");
            request.Content = new StringContent(JsonSerializer.Serialize(registrationRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await LoginAsync(email, password);
            }
            else
            {
                // Registration failed, handle error
                return false;
            }
        }

        private async Task GetClaims(string tokenStr)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenStr);
            var displayNameClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (displayNameClaim != null)
            {
                // User's role found, update userRole variable
                Authority.DisplayName = displayNameClaim.Value;
            }

            // Access the claims from the token
            var roleClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim != null)
            {
                // User's role found, update userRole variable
                Authority.Role = roleClaim.Value;
            }
        }
    }
}
