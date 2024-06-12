using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly ApplicationDbContext _context;

    public TodoItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> AddTodoItemAsync(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync()
    {
        return await _context.TodoItems
            .Include(t => t.AssignedToUser)
            .Include(t => t.WorkIntervals)
            .ToListAsync();
    }

    public async Task<TodoItem> GetTodoItemByIdAsync(int id)
    {
        return await _context.TodoItems
            .Include(t => t.AssignedToUser)
            .Include(t => t.WorkIntervals)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task UpdateTodoItemAsync(TodoItem todoItem)
    {
        _context.Entry(todoItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem != null)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }

    public Task AddAsync(TodoItem todoItem)
    {
        throw new NotImplementedException();
    }
}
