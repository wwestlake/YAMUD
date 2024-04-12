using FluentResults;
using LagDaemon.YAMUD.Model.Map;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.WebAPI.Services.RoomServices
{
    public interface IRoomManagementService
    {
        Task<Result<Room>> CreateRoom(int x, int y, int level, string name, string description, IEnumerable<Exit> exits);
        Task<Result<Room>> FindRoom(int x, int y, int level);

        Task<Result<IEnumerable<Room>>> GetRooms(Expression<Func<Room, bool>> predicate);
        Task<Result<IEnumerable<Room>>> GetRooms(Expression<Func<Room, bool>> predicate,
                                                       Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null);

    }
}
