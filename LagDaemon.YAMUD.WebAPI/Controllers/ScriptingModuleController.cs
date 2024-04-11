using LagDaemon.YAMUD.Model.Scripting;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.WebAPI.Services.Scripting;
using LagDaemon.YAMUD.WebAPI.Services.ScriptingServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LagDaemon.YAMUD.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScriptingModuleController : Controller
    {
        private IScriptingModuleService _scriptingModuleService;

        public ScriptingModuleController(IScriptingModuleService scriptingModuleService)
        {
            _scriptingModuleService = scriptingModuleService;
        }

        // GET: api/UserAccount
        [Authorize]
        [HttpGet("GetAllModules")]
        public async Task<IActionResult> GetAllModules()
        {
            var result = await _scriptingModuleService.GetAll();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetModuleById/{id}")]
        public async Task<IActionResult> GetModuleById(Guid id)
        {
            var result = await _scriptingModuleService.Get(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Objecty ID not found");
            }
        }

        [Authorize]
        [HttpPost("CreateNewModule")]
        public async Task<IActionResult> CreateNewModule([FromBody] CodeModule module)
        {
            if (await _scriptingModuleService.Create(module) == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Module was not created");
            }
        }

        [Authorize]
        [HttpPost("UpdateModule")]
        public async Task<IActionResult> UpdateModule([FromBody] CodeModule module)
        {
            await _scriptingModuleService.Update(module);
            return Ok();
        }


    }
}
