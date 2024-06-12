using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkIntervalController : ControllerBase
{
    private readonly WorkIntervalService _workIntervalService;

    public WorkIntervalController(WorkIntervalService workIntervalService)
    {
        _workIntervalService = workIntervalService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWorkInterval([FromBody] CreateWorkIntervalDTO workIntervalDto)
    {
        var result = await _workIntervalService.AddWorkIntervalAsync(workIntervalDto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWorkIntervals()
    {
        var result = await _workIntervalService.GetAllWorkIntervalsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkIntervalById(int id)
    {
        var result = await _workIntervalService.GetWorkIntervalByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("todoItem/{todoItemId}")]
    public async Task<IActionResult> GetWorkIntervalsByTodoItemId(int todoItemId)
    {
        var result = await _workIntervalService.GetWorkIntervalsByTodoItemIdAsync(todoItemId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkInterval(int id, [FromBody] WorkInterval workInterval)
    {
        if (id != workInterval.Id)
        {
            return BadRequest();
        }

        await _workIntervalService.UpdateWorkIntervalAsync(workInterval);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkInterval(int id)
    {
        await _workIntervalService.DeleteWorkIntervalAsync(id);
        return NoContent();
    }
}
