using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class WorkIntervalRepository : IWorkIntervalRepository
{
    private readonly ApplicationDbContext _context;

    public WorkIntervalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkInterval> AddWorkIntervalAsync(WorkInterval workInterval)
    {
        _context.WorkIntervals.Add(workInterval);
        await _context.SaveChangesAsync();
        return workInterval;
    }

    public async Task<IEnumerable<WorkInterval>> GetAllWorkIntervalsAsync()
    {
        return await _context.WorkIntervals
            .Include(w => w.User)
            .Include(w => w.TodoItem)
            .ToListAsync();
    }

    public async Task<WorkInterval> GetWorkIntervalByIdAsync(int id)
    {
        return await _context.WorkIntervals
            .Include(w => w.User)
            .Include(w => w.TodoItem)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task UpdateWorkIntervalAsync(WorkInterval workInterval)
    {
        _context.Entry(workInterval).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWorkIntervalAsync(int id)
    {
        var workInterval = await _context.WorkIntervals.FindAsync(id);
        if (workInterval != null)
        {
            _context.WorkIntervals.Remove(workInterval);
            await _context.SaveChangesAsync();
        }
    }

    public Task<IEnumerable<WorkInterval>> GetWorkIntervalsByTodoItemIdAsync(int todoItemId)
    {
        throw new NotImplementedException();
    }
}
