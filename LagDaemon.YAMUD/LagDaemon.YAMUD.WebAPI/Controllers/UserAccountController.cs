using FluentResults;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Models;
using LagDaemon.YAMUD.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUserAccounts()
        {
            try
            {
                var isFailed = false;
                IEnumerable<UserAccount> userAccounts = new List<UserAccount>().AsEnumerable();
                IEnumerable<IError> errors = new List<IError>().AsEnumerable();
                (await _userAccountService.GetAllUserAccounts()).OnSuccess(x =>
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

        [Authorize]
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserAccountById(Guid id)
        {
            var userAccount = _userAccountService.GetUserAccountById(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            return Ok(userAccount);
        }

        [Authorize]
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

        
        [HttpGet("VerifyEmail/{id}/{token}")]
        public async Task<IActionResult> VerifyUser(Guid id, Guid token)
        {
            var result = await _userAccountService.VerifyUserEmail(id, token);

            if (result.IsSuccess)
            {
                return Ok(new { message = "User email verified" });
            }
            else
            {
                return BadRequest(new { message = "Verification failed" });
            }
        }


        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateUserAccount([FromBody] UserAccount userAccount)
        {
            IActionResult result = Ok(string.Empty);
            var createdUserAccount = await _userAccountService.CreateUserAccount(userAccount);
            createdUserAccount.OnSuccess(x =>
            {
                result = CreatedAtAction(nameof(GetUserAccountById), new { id = createdUserAccount.Value.ID }, createdUserAccount);

            }).OnFailure(x =>
            {
                result = Ok(x);
            });

            return result;
        }

        [Authorize]
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUserAccount(Guid id, UserAccount updatedUserAccount)
        {
            var existingUserAccount = _userAccountService.GetUserAccountById(id);
            if (existingUserAccount == null)
            {
                return NotFound();
            }
            _userAccountService.UpdateUserAccount(id, updatedUserAccount);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUserAccount(Guid id)
        {
            var result = await _userAccountService.DeleteUserAccount(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginModel userLogin)
        {
            var result = await _userAccountService.AuthenticateAsync(userLogin.EmailAddress, userLogin.Password);

            if (result.IsSuccess)
            {
                return Ok(new { token = result.Value });
            }
            else
            {
                return Unauthorized(new { message = result.Errors });
            }
        }

    }
}
