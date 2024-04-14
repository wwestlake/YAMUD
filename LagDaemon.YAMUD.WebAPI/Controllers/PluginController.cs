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
        [HttpGet]
        public IActionResult GetPlugins()
        {
            return Ok(_pluginService.GetPlugins());
        }

        [Authorize]
        [HttpDelete("{pluginId}")]
        public async Task<IActionResult> UnloadPlugin(Guid pluginId)
        {
            await _pluginService.UnloadPluginAsync(pluginId);
            return Ok();
        }
    }
}
