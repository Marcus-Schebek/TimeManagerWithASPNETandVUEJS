using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }
    [HttpPost]
    public async Task<IActionResult> AddTodoItem([FromBody] CreateTodoItemDTO todoItemDto)
    {
        var result = await _todoService.AddTodoItemAsync(todoItemDto);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllTodoItems()
    {
        var result = await _todoService.GetAllTodoItemsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoItemById(int id)
    {
        var result = await _todoService.GetTodoItemByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(int id, [FromBody] TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }

        await _todoService.UpdateTodoItemAsync(todoItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        await _todoService.DeleteTodoItemAsync(id);
        return NoContent();
    }
}
