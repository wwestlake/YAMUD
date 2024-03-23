using System;
using FluentResults;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Services;
using LagDaemon.YAMUD.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

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
        [HttpGet]
        public IActionResult GetAllUserAccounts()
        {
            try
            {
                var isFailed = false;
                IEnumerable<UserAccount> userAccounts = new List<UserAccount>().AsEnumerable();
                IEnumerable<IError> errors = new List<IError>().AsEnumerable();
                _userAccountService.GetAllUserAccounts().OnSuccess( x => {
                    userAccounts = x;    
                }).OnFailure( x => { 
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

        [HttpGet("{id}")]
        public IActionResult GetUserAccount(Guid id)
        {
            var userAccount = _userAccountService.GetUserAccount(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            return Ok(userAccount);
        }

        [HttpPost]
        public IActionResult CreateUserAccount([FromBody]UserAccount userAccount)
        {
            IActionResult result = Ok("");
            var createdUserAccount = _userAccountService.CreateUserAccount(userAccount);
            createdUserAccount.OnSuccess(x =>
            {
                result = CreatedAtAction(nameof(GetUserAccount), new { id = createdUserAccount.Value.ID }, createdUserAccount);

            }).OnFailure( x => {
                result = Ok(x);
            });

            return result;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserAccount(Guid id, UserAccount updatedUserAccount)
        {
            var existingUserAccount = _userAccountService.GetUserAccount(id);
            if (existingUserAccount == null)
            {
                return NotFound();
            }
            _userAccountService.UpdateUserAccount(id, updatedUserAccount);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserAccount(Guid id)
        {
            var existingUserAccount = _userAccountService.GetUserAccount(id);
            if (existingUserAccount == null)
            {
                return NotFound();
            }
            _userAccountService.DeleteUserAccount(id);
            return NoContent();
        }
    }
}
