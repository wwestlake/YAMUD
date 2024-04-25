using LagDaemon.YAMUD.Model.Communication;
using System.Collections.Concurrent;

namespace LagDaemon.YAMUD.WebClient.Services
{
    public static class InMemoryStorageService
    {
        private const string RoomChatMessageKey = "RoomChatMessages";
        private static ConcurrentDictionary<string, dynamic> _storage = new ConcurrentDictionary<string, dynamic>();

        public static void Store<T>(string key, T value) 
        { 
            _storage.AddOrUpdate(key, value, (k, v) => value);
        }

        public static T GetT<T>(string key)
        {
            if (_storage.TryGetValue(key, out dynamic value))
            {
                return (T)value;
            }
            return default;
        }

        public static void Remove(string key)
        {
            _storage.TryRemove(key, out _);
        }

        public static void Clear()
        {
            _storage.Clear();
        }

        public static void AddChatMessage(RoomChatMessage message)
        {
            if (!_storage.ContainsKey(RoomChatMessageKey))
            {
                var list = new ConcurrentBag<RoomChatMessage>();
                list.Add(message);
                Store(RoomChatMessageKey, list);
            }
            else
            {
                var list = GetT<ConcurrentBag<RoomChatMessage>>(RoomChatMessageKey);
                list.Add(message);
                Store(RoomChatMessageKey, list);
            }
        }

        public static IEnumerable<RoomChatMessage> GetRoomChatMessages()
        {
            var result = GetT<ConcurrentBag<RoomChatMessage>>(RoomChatMessageKey);
            if (result == default) return new ConcurrentBag<RoomChatMessage>();
            return result;
        }
    }
}
