using FluentResults;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Utilities;

namespace LagDaemon.YAMUD.WebAPI.Services.RoomServices;

public class RoomCreationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Room> _roomRepo;
    private readonly ILogger _logger;
    private readonly IUserAccountService _userAccountService;
    private UserAccount _user;
    private Guid _userId;

    public RoomCreationService(ILogger logger, IUserAccountService userAccountService, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _roomRepo = _unitOfWork.GetRepository<Room>();
        _logger = logger;
        _userAccountService = userAccountService;
        _userAccountService.GetCurrentUser().Result.OnSuccess(x => {
                _user = x;
            }).OnFailure(x => { 
                
            });
        _userId = _user.ID;
    }

    [Security(Model.UserAccountRoles.Admin)]
    public async Task<Result<Room>> CreateRoom(int x, int y, int level, string name, string description, IEnumerable<Exit> exits)
    {

        var existingRoom = (await _roomRepo.GetAsync(room => room.Address.X == x && room.Address.Y == y && room.Address.Level == level)).FirstOrDefault();
        if (existingRoom == null)
        {
            var room = new Room()
            {
                Address = new RoomAddress() { X = x, Y = y, Level = level },
                Name = name,
                Description = description,
                Owner = _userId,
                Exits = new List<Exit>(exits),
                Id = Guid.NewGuid()
            };
            _unitOfWork.SaveChanges();
            return Result.Ok(room);
        }
        else
        {
            return Result.Fail($"Room already exists at location {x},{y},{level}");
        }
    }

}
