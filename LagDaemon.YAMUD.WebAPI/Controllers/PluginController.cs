using LagDaemon.YAMUD.Model.Automation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LagDaemon.YAMUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private readonly PluginService _pluginService;

        public PluginController(PluginService pluginService)
        {
            _pluginService = pluginService;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetPlugins()
        {
            return Ok(_pluginService.GetPlugins());
        }

        [Authorize]
        [HttpGet("Stop/{pluginId}")]
        public async Task<IActionResult> UnloadPlugin(Guid pluginId)
        {
            await _pluginService.UnloadPluginAsync(pluginId);
            return Ok();
        }

        [Authorize]
        [HttpGet("Start/{pluginId}")]
        public async Task<IActionResult> LoadPlugin(Guid pluginId)
        {
            await _pluginService.LoadPluginAsync(pluginId);
            return Ok();
        }


    }
}
