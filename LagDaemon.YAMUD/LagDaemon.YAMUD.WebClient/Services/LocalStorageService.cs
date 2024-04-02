using Microsoft.JSInterop;
using System.Text.Json;

namespace LagDaemon.YAMUD.WebClient.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetItemAsync(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task SetItemAsync(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetObjectAsync<T>(string key, T item, int expirationTimeInMinutes = 60)
        {
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);
            string serializedItem = JsonSerializer.Serialize(new LocalStorageItem<T>
            {
                ExpirationTime = expirationTime,
                Item = item
            });
            await SetItemAsync(key, serializedItem);
        }

        public async Task<LocalStorageItem<T>> GetObjectAsync<T>(string key)
        {
            var localItem = await GetItemAsync(key);
            if (localItem == null)
            {
                throw new ApplicationException($"Item with key '{key}' not found in LocalStorage");
            }

            var storedItem = JsonSerializer.Deserialize<LocalStorageItem<T>>(localItem);
            if (storedItem == null)
            {
                throw new ApplicationException($"Item with key '{key}' could not be deserialized from LocalStorage");
            }
            return storedItem;
        }


        public async Task CheckTokenExpirationAsync(CancellationToken token)
        {
            while (! token.IsCancellationRequested)
            {
                // Get all keys stored in LocalStorage
                var keysJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "keys");
                if (!string.IsNullOrEmpty(keysJson))
                {
                    // Deserialize the keys JSON
                    var keys = JsonSerializer.Deserialize<List<string>>(keysJson);

                    // Iterate over each key to check its corresponding item's expiration time
                    foreach (var key in keys)
                    {
                        // Get the stored item
                        string itemJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
                        if (!string.IsNullOrEmpty(itemJson))
                        {
                            // Deserialize the item object
                            var item = JsonSerializer.Deserialize<LocalStorageItem<dynamic>>(itemJson);
                            DateTime expirationTime = item.ExpirationTime;

                            // Check if the item has expired
                            if (DateTime.UtcNow >= expirationTime)
                            {
                                // Item has expired, remove it from LocalStorage
                                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
                            }
                        }
                    }
                }

                // Wait for some time before checking again (e.g., every minute)
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
