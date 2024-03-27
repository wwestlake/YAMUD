using FluentEmail.Core;
using FluentResults;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Models;
using LagDaemon.YAMUD.WebAPI.Services;
using LagDaemon.YAMUD.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LagDaemon.YAMUD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserAccountController : ControllerBase
{
    private readonly IUserAccountService _userAccountService;
    private readonly UserAccountMask _userAccountMask;

    public UserAccountController(IUserAccountService userAccountService, UserAccountMask userAccountMask)
    {
        _userAccountService = userAccountService;
        _userAccountMask = userAccountMask;
    }

    // GET: api/UserAccount
    [Authorize]
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUserAccounts()
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
                return Ok(userAccounts.Select(x =>  _userAccountMask.Map(x)));
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("GetUserById/{id}")]
    public async Task<IActionResult> GetUserAccountById(Guid id)
    {
        try
        {
            var userAccount = await _userAccountService.GetUserAccountById(id);
            UserAccount result = null;
            bool failed = false;
            IEnumerable<IError> errors = new List<IError>().AsEnumerable();

            if (userAccount == null)
            {
                return NotFound();
            }

            userAccount.OnSuccess(x =>
            {
                result = x;
            }).OnFailure(x =>
            {
                errors = x;
            });

            if (failed)
            {
                return Ok(errors);
            }
            else
            {
                return Ok(_userAccountMask.Map(result));
            }

        } catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("GetUserByEmail/{email}")]
    public async Task<IActionResult> GetUserAccountByEmail(string email)
    {
        try
        {
            var userAccount = await _userAccountService.GetUserAccountByEmail(email);
            UserAccount result = null;
            bool failed = false;
            IEnumerable<IError> errors = new List<IError>().AsEnumerable();

            if (userAccount == null)
            {
                return NotFound();
            }

            userAccount.OnSuccess(x =>
            {
                result = x;
            }).OnFailure(x =>
            {
                errors = x;
            });

            if (failed)
            {
                return Ok(errors);
            }
            else
            {
                return Ok(_userAccountMask.Map(result));
            }

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
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
    public async Task<IActionResult> CreateUserAccount([FromBody] CreateUserModel userAccount)
    {
        IActionResult result = Ok(string.Empty);
        var createdUserAccount = await _userAccountService.CreateUserAccount(userAccount);
        createdUserAccount.OnSuccess(x =>
        {
            result = CreatedAtAction(nameof(GetUserAccountById), new { id = createdUserAccount.Value.ID }, _userAccountMask.Map(x));

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
