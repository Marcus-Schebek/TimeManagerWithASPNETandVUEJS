using api.Models;
public interface IWorkIntervalRepository
{
    Task<WorkInterval> AddWorkIntervalAsync(WorkInterval workInterval);
    Task<IEnumerable<WorkInterval>> GetWorkIntervalsByTodoItemIdAsync(int todoItemId);
    Task<WorkInterval> GetWorkIntervalByIdAsync(int id);
    Task<IEnumerable<WorkInterval>> GetAllWorkIntervalsAsync();
    Task UpdateWorkIntervalAsync(WorkInterval workInterval);
    Task DeleteWorkIntervalAsync(int id);
}
