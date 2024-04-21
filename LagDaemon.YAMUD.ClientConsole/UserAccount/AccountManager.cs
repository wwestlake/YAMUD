using LagDaemon.YAMUD.ClientConsole.ConsoleIO;
using LagDaemon.YAMUD.Model.User;
using Polly;
using System.Net.Http.Json;
using System.Text.Json;

namespace LagDaemon.YAMUD.ClientConsole.UserAccount
{
    public class AccountManager
    {
        private HttpClient _mudHubClient;
        private ConsoleInputHandler _consoleHander = new ConsoleInputHandler();
        private bool _authenticated;
        
        public string? Token { get; set; }

        public AccountManager(IHttpClientFactory clientFactory)
        {
            _mudHubClient = clientFactory.CreateClient("MudHub");
        }

        public async Task<bool> CreateAccount()
        {
         getdata:
            var emailAddress = _consoleHander.GetInput(   "   Email Address: ");
            var displayName = _consoleHander.GetInput(    "    Display Name: ");
            var password1 = _consoleHander.GetSecretInput("        Password: ");
            var password2 = _consoleHander.GetSecretInput("Reenter Password: ");

            if (password1 != password2)
            {
                Console.WriteLine("Passwords do not match, try again");
                goto getdata;
            }   

            var httpResponse = await _mudHubClient.PostAsJsonAsync("UserAccount/CreateNewUser", new CreateUserModel() { 
                Email = emailAddress,
                DisplayName = displayName,
                Password = password1
            });
            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Account for {displayName} created.");
                return true;
            } else
            {
                Console.WriteLine($"Account for {displayName} not created.");
                Console.WriteLine($"{httpResponse.StatusCode} - {httpResponse.ReasonPhrase}");
                return false;
            }
        }

        public async Task<bool> AuthenticatePipeline(string userId, string password)
        {
            ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new Polly.Retry.RetryStrategyOptions())
                .AddTimeout(TimeSpan.FromSeconds(10))
                .Build();

            await pipeline.ExecuteAsync(async token => { await AuthenticateUser(userId, password); });
            return true;    
        }

        public async Task<bool> AuthenticateUser(string userId, string password)
        {
            try
            {
                var userLogin = new UserLoginModel { EmailAddress = userId, Password = password };
                // Send the user credentials to the MudHub API for authentication
                var httpResponse = await _mudHubClient.PostAsJsonAsync("UserAccount/Authenticate", userLogin);

                if (httpResponse.IsSuccessStatusCode)
                {
                    // Authentication successful, retrieve token from response
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var responseJson = JsonSerializer.Deserialize<JsonDocument>(responseContent);

                    if (responseJson.RootElement.TryGetProperty("token", out var tokenElement))
                    {
                        var token = tokenElement.GetString();

                        // Set authenticated flag and token
                        _authenticated = true;
                        Token = token;
                        Console.WriteLine("Authentication successful.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Token not found in response.");
                    }
                }
                else
                {
                    // Authentication failed, display error message
                    Console.WriteLine($"Authentication failed: {httpResponse.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error occurred during authentication: {ex.Message}");
                return false;
            }
            return false;
        }

        public async Task<bool> AuthenticateUser()
        {
            int tries = 0;
            while (!_authenticated && tries < 3)
            {
                var userId = _consoleHander.GetInput("User ID: ");
                var password = _consoleHander.GetSecretInput("Password: ");
                var userLogin = new UserLoginModel { EmailAddress = userId, Password = password };

                _authenticated = await AuthenticateUser(userId, password);
                tries++;
            }
            // Maximum attempts reached
            Console.WriteLine("Maximum authentication attempts reached. Exiting...");
            return false;
        }
    }


}
