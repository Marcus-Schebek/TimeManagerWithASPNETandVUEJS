using api.Models;
using api.Repositories;

public class TodoService
{
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly IUserRepository _userRepository;
    private readonly IWorkIntervalRepository _workIntervalRepository;

    public TodoService(ITodoItemRepository todoItemRepository, IUserRepository userRepository, IWorkIntervalRepository workIntervalRepository)
    {
        _todoItemRepository = todoItemRepository;
        _userRepository = userRepository;
        _workIntervalRepository = workIntervalRepository;
    }

    public async Task<TodoItem> AddTodoItemAsync(CreateTodoItemDTO todoItemDto)
    {
        var user = await _userRepository.GetByIdAsync(todoItemDto.AssignedToUserId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var workIntervalStart = DateTime.SpecifyKind(todoItemDto.WorkInterval.StartTime, DateTimeKind.Utc);
        var workIntervalEnd = DateTime.SpecifyKind(todoItemDto.WorkInterval.EndTime, DateTimeKind.Utc);

        var todoItem = new TodoItem
        {
            Description = todoItemDto.Description,
            AssignedToUser = user,
            WorkIntervals = new List<WorkInterval>()
        };

        var createdTodoItem = await _todoItemRepository.AddTodoItemAsync(todoItem);

        var workInterval = new WorkInterval
        {
            StartTime = workIntervalStart,
            EndTime = workIntervalEnd,
            UserId = user.Id,
            TodoItemId = createdTodoItem.Id,
            User = user,
            TodoItem = createdTodoItem
        };

        await _workIntervalRepository.AddWorkIntervalAsync(workInterval);

        createdTodoItem.WorkIntervals.Add(workInterval);
        await _todoItemRepository.UpdateTodoItemAsync(createdTodoItem);

        return createdTodoItem;
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync()
    {
        return await _todoItemRepository.GetAllTodoItemsAsync();
    }

    public async Task<TodoItem> GetTodoItemByIdAsync(int id)
    {
        return await _todoItemRepository.GetTodoItemByIdAsync(id);
    }

    public async Task UpdateTodoItemAsync(TodoItem todoItem)
    {
        await _todoItemRepository.UpdateTodoItemAsync(todoItem);
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        await _todoItemRepository.DeleteTodoItemAsync(id);
    }

    public async Task<WorkInterval> AddWorkIntervalAsync(CreateWorkIntervalDTO workIntervalDto)
    {
        var user = await _userRepository.GetByIdAsync(workIntervalDto.UserId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var todoItem = await _todoItemRepository.GetTodoItemByIdAsync(workIntervalDto.TodoItemId);
        if (todoItem == null)
        {
            throw new ArgumentException("TodoItem not found");
        }

        var workInterval = new WorkInterval
        {
            StartTime = DateTime.SpecifyKind(workIntervalDto.StartTime, DateTimeKind.Utc),
            EndTime = DateTime.SpecifyKind(workIntervalDto.EndTime, DateTimeKind.Utc),
            UserId = workIntervalDto.UserId,
            TodoItemId = workIntervalDto.TodoItemId,
            User = user,
            TodoItem = todoItem
        };

        todoItem.WorkIntervals.Add(workInterval);
        await _todoItemRepository.UpdateTodoItemAsync(todoItem);

        return await _workIntervalRepository.AddWorkIntervalAsync(workInterval);
    }
}
