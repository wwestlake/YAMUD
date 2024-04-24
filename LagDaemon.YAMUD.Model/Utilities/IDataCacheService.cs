using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.User;
using System.Collections.Concurrent;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public interface IDataCacheService
    {
        ConcurrentDictionary<Guid, Room> ActiveRooms { get; set; }
        ConcurrentDictionary<Guid, UserAccount> CurrentUsers { get; set; }

        ConcurrentDictionary<string, IList<Guid>> ChatGroups { get; set; }
    }
}