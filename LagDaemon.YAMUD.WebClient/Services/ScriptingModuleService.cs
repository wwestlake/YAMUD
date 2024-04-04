using LagDaemon.YAMUD.Model.Scripting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace LagDaemon.YAMUD.WebClient.Services
{
    public class ScriptingModuleService : IScriptingModuleService
    {
        private HttpClient _httpClient;
        private IAuthenticationService _authenticationService;

        public ScriptingModuleService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _authenticationService = authenticationService;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authenticationService.Authority.AuthToken.token);
        }


        public async Task<IEnumerable<Module>> GetAllModules()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7214/api/ScriptingModule/GetAllModules");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationService.Authority.AuthToken.token);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<Module>>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            return null;
        }

        public async Task<Module> GetModuleById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Module>($"api/ScriptingModule/GetModuleById/{id}");
        }

        public async Task CreateNewModule(Module module)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7214/api/ScriptingModule/CreateNewModule");
            request.Content = new StringContent(JsonSerializer.Serialize(module), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationService.Authority.AuthToken.token);
            var response = await _httpClient.SendAsync(request);
        }

        public async Task UpdateModule(Module module)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7214/api/ScriptingModule/UpdateModule");
            request.Content = new StringContent(JsonSerializer.Serialize(module), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationService.Authority.AuthToken.token);
            var response = await _httpClient.SendAsync(request);
        }

        public async Task DeleteModule(Guid id)
        {
            await _httpClient.DeleteAsync($"api/ScriptingModule/DeleteModule/{id}");
        }
    }
}
