using api.Models;
public interface ITodoItemRepository
{
    Task<TodoItem> AddTodoItemAsync(TodoItem todoItem);
    Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync();
    Task<TodoItem> GetTodoItemByIdAsync(int id);
    Task UpdateTodoItemAsync(TodoItem todoItem);
    Task DeleteTodoItemAsync(int id);
    Task AddAsync(TodoItem todoItem);
}
