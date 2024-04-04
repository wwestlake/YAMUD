namespace LagDaemon.YAMUD.WebClient.Services
{
    public interface ILocalStorageService
    {
        Task<string> GetItemAsync(string key);
        Task SetItemAsync(string key, string value);
        Task RemoveItemAsync(string key);
        Task SetObjectAsync<T>(string key, T item, int expirationTimeInMinutes = 60);
        Task<LocalStorageItem<T>> GetObjectAsync<T>(string key);
        Task CheckTokenExpirationAsync(CancellationToken token);
    }
}
