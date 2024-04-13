using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.WebAPI.Services.CharacterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LagDaemon.YAMUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuest(Guid id)
        {
            var result = await _questService.GetQuest(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Errors);
        }

        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuest(Quest quest)
        {
            var result = await _questService.CreateQuest(quest);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetQuest), new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuest(Guid id, Quest quest)
        {
            if (id != quest.Id)
            {
                return BadRequest("Id mismatch");
            }

            var result = await _questService.UpdateQuest(quest);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteQuest(Guid id)
        {
            var result = await _questService.DeleteQuest(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(result.Errors);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuests()
        {
            var result = await _questService.GetAllQuests();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Errors);
        }
    }
}
