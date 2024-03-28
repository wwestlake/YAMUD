using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.WebClient.Services
{
    public interface IIndexedDbService
    {
        Task<bool> SaveData(string key, string value);
        Task<string> GetData(string key);
    }

    public class IndexedDbService : IIndexedDbService
    {
        private readonly IJSRuntime _jsRuntime;

        public IndexedDbService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> SaveData(string key, string value)
        {
            return await _jsRuntime.InvokeAsync<bool>("indexedDBService.saveData", key, value);
        }

        public async Task<string> GetData(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("indexedDBService.getData", key);
        }
    }
}
