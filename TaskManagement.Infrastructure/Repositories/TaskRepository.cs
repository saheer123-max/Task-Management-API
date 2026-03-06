using Microsoft.EntityFrameworkCore;

using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskItem>> GetTasksByUser(string userId)
    {
        return await _context.Tasks
            .Where(t => t.OwnerUserId == userId)
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetAllTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetById(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task Update(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
}