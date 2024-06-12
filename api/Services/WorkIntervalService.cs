using api.Models;
using api.Repositories;

public class WorkIntervalService
{
    private readonly IWorkIntervalRepository _workIntervalRepository;
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly IUserRepository _userRepository;

    public WorkIntervalService(IWorkIntervalRepository workIntervalRepository, ITodoItemRepository todoItemRepository, IUserRepository userRepository)
    {
        _workIntervalRepository = workIntervalRepository;
        _todoItemRepository = todoItemRepository;
        _userRepository = userRepository;
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

        return await _workIntervalRepository.AddWorkIntervalAsync(workInterval);
    }

    public async Task<IEnumerable<WorkInterval>> GetAllWorkIntervalsAsync()
    {
        return await _workIntervalRepository.GetAllWorkIntervalsAsync();
    }

    public async Task<WorkInterval> GetWorkIntervalByIdAsync(int id)
    {
        return await _workIntervalRepository.GetWorkIntervalByIdAsync(id);
    }

    public async Task<IEnumerable<WorkInterval>> GetWorkIntervalsByTodoItemIdAsync(int todoItemId)
    {
        return await _workIntervalRepository.GetWorkIntervalsByTodoItemIdAsync(todoItemId);
    }

    public async Task UpdateWorkIntervalAsync(WorkInterval workInterval)
    {
        await _workIntervalRepository.UpdateWorkIntervalAsync(workInterval);
    }

    public async Task DeleteWorkIntervalAsync(int id)
    {
        await _workIntervalRepository.DeleteWorkIntervalAsync(id);
    }
}
