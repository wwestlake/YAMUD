using FluentResults;
using LagDaemon.YAMUD.Model.Map;
using LagDaemon.YAMUD.WebAPI.Services.RoomServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LagDaemon.YAMUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomManagementService _roomManagementService;

        public RoomController(IRoomManagementService roomManagementSerice)
        {
            _roomManagementService = roomManagementSerice;
        }

        [Authorize]
        [HttpGet("FindRoom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindRoom(int x, int y, int level)
        {
            var result = await _roomManagementService.FindRoom(x, y, level);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, result.Errors);
            }
        }

        [Authorize]
        [HttpPost("CreateRoom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoom(int x, int y, int level, string name, string description, IEnumerable<Exit> exits)
        {
            var result = await _roomManagementService.CreateRoom(x, y, level, name, description, exits);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
            }
        }

        [Authorize]
        [HttpGet("GetRooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRooms()
        {
            var result = await _roomManagementService.GetRooms(_ => true);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, result.Errors);
            }
        }

    }
}
