using FluentResults;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Utilities;
using System.Linq.Expressions;
using ThothLog.Services;

namespace LagDaemon.YAMUD.WebAPI.Services.RoomServices;

public class RoomManagementService : IRoomManagementService
{
    private const string IncludeProperties
        = "Address,Exits,Items";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Room> _roomRepo;
    private readonly ILoggingService _logger;
    private readonly IUserAccountService _userAccountService;
    private UserAccount _user;
    private Guid _userId;

    public RoomManagementService(ILoggingService logger, IUserAccountService userAccountService, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _roomRepo = _unitOfWork.GetRepository<Room>();
        _logger = logger;
        _userAccountService = userAccountService;
        _userAccountService.GetCurrentUser().Result.OnSuccess(x =>
        {
            _user = x;
        }).OnFailure(x =>
        {

        });
        _userId = _user.ID;

        if (GetRooms(x => true).Result.Value.Count() == 0)
        {
            CreateRoom(0, 0, 0, "The Lobby", "A marble hall of large proportions", new List<Exit>()).Wait();
        }


    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result<Room>> FindRoom(int x, int y, int level)
    {
        var existingRoom = (await _roomRepo.GetAsync(room => room.Address.X == x && room.Address.Y == y && room.Address.Level == level, null, IncludeProperties)).FirstOrDefault();
        if (existingRoom != null)
        {
            return Result.Ok(existingRoom);
        }
        else
        {
            string errorMessage = $"No room found at RoomLocation({x},{y},{level})";
            return Result.Fail(errorMessage);
        }
    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result<Room>> CreateRoom(int x, int y, int level, string name, string description, IEnumerable<Exit> exits)
    {
        Room? room = null;
        (await FindRoom(x, y, level)).OnSuccess(room =>
        {
            // we do nothing, the room exists so this method fails            
        }).OnFailure(msg =>
        {
            room = new Room()
            {
                Address = new RoomAddress() { X = x, Y = y, Level = level },
                Name = name,
                Description = description,
                Owner = _userId,
                Exits = new List<Exit>(exits),
                Id = Guid.NewGuid()
            };
            _roomRepo.Insert(room);
            _unitOfWork.SaveChanges();
        });
        return room != null ? Result.Ok(room) : Result.Fail($"Room at Location({x},{y},{level}) already exists!");
    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result<IEnumerable<Room>>> GetRooms(Expression<Func<Room, bool>> predicate)
    {
        return Result.Ok(await _roomRepo.GetAsync(predicate, null, IncludeProperties));
    }

    [Security(UserAccountRoles.Admin)]
    public async Task<Result<IEnumerable<Room>>> GetRooms(Expression<Func<Room, bool>> predicate,
                                                       Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null)
    {
        try
        {
            var rooms = await _roomRepo.GetAsync(predicate, orderBy, IncludeProperties);
            return Result.Ok(rooms);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while getting rooms: {ex.Message}");
            return Result.Fail<IEnumerable<Room>>("An error occurred while getting rooms.");
        }
    }

}
