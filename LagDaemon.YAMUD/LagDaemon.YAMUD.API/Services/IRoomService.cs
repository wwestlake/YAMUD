using LagDaemon.YAMUD.Model.Map;
using System;
using System.Linq;

namespace LagDaemon.YAMUD.API.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomsByLevel(int level);
    }
}
