using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.User;
using System.Collections.Concurrent;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public class DataCacheService : IDataCacheService
    {
        public ConcurrentDictionary<Guid, UserAccount> CurrentUsers { get; set; } 
            = new ConcurrentDictionary<Guid, UserAccount>();
        public ConcurrentDictionary<Guid, Room> ActiveRooms { get; set; } 
            = new ConcurrentDictionary<Guid, Room>();
        public ConcurrentDictionary<string, IList<Guid>> ChatGroups { get; set; }
            = new ConcurrentDictionary<string, IList<Guid>>();
    }
}
