using FluentResults;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LagDaemon.YAMUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        // GET: api/UserAccount
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUserAccounts()
        {
            try
            {
                var isFailed = false;
                IEnumerable<UserAccount> userAccounts = new List<UserAccount>().AsEnumerable();
                IEnumerable<IError> errors = new List<IError>().AsEnumerable();
                _userAccountService.GetAllUserAccounts().OnSuccess(x =>
                {
                    userAccounts = x;
                }).OnFailure(x =>
                {
                    isFailed = true;
                    errors = x;
                });
                if (isFailed)
                {
                    return Ok(errors);
                }
                else
                {
                    return Ok(userAccounts);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserAccountById(string id)
        {
            var userAccount = _userAccountService.GetUserAccountById(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            return Ok(userAccount);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public IActionResult GetUserAccountByEmail(string email)
        {
            var userAccount = _userAccountService.GetUserAccountByEmail(email);
            if (userAccount == null)
            {
                return NotFound();
            }
            return Ok(userAccount);
        }


        [HttpPost("CreateNewUser")]
        public IActionResult CreateUserAccount([FromBody] UserAccount userAccount)
        {
            IActionResult result = Ok(string.Empty);
            var createdUserAccount = _userAccountService.CreateUserAccount(userAccount);
            createdUserAccount.OnSuccess(x =>
            {
                result = CreatedAtAction(nameof(GetUserAccountById), new { id = createdUserAccount.Value.ID }, createdUserAccount);

            }).OnFailure(x =>
            {
                result = Ok(x);
            });

            return result;
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUserAccount(string id, UserAccount updatedUserAccount)
        {
            var existingUserAccount = _userAccountService.GetUserAccountById(id);
            if (existingUserAccount == null)
            {
                return NotFound();
            }
            _userAccountService.UpdateUserAccount(id, updatedUserAccount);
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUserAccount(string id)
        {
            var result = _userAccountService.DeleteUserAccount(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
