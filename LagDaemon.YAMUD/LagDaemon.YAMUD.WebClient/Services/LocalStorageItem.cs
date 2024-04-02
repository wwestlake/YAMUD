
namespace LagDaemon.YAMUD.WebClient.Services
{
    public class LocalStorageItem<T>
    {
        public DateTime ExpirationTime { get; set; }
        public T Item { get; set; }
    }
}