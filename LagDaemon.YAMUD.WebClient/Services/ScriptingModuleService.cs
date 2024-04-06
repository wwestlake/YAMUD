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


        public async Task<IEnumerable<CodeModule>> GetAllModules()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7214/api/ScriptingModule/GetAllModules");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationService.Authority.AuthToken.token);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<CodeModule>>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            return null;
        }

        public async Task<CodeModule> GetModuleById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CodeModule>($"api/ScriptingModule/GetModuleById/{id}");
        }

        public async Task CreateNewModule(CodeModule module)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7214/api/ScriptingModule/CreateNewModule");
            request.Content = new StringContent(JsonSerializer.Serialize(module), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationService.Authority.AuthToken.token);
            var response = await _httpClient.SendAsync(request);
        }

        public async Task UpdateModule(CodeModule module)
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
