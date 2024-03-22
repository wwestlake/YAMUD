using LagDaemon.YAMUD.Model.Map;

namespace LagDaemon.YAMUD.API.Services
{
    public interface IMongoRoomService
    {
        Task<Room> CreateRoom(Room room);
        Task DeleteRoom(Guid id);
        Task<Room> GetRoomById(Guid id);
        Task<IEnumerable<Room>> GetRoomsByLevel(int level);
        Task UpdateRoom(Room room);
    }
}
