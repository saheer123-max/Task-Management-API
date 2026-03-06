
using TaskManagement.Domain.Interfaces;

public class FakeTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public Task AddAsync(TaskItem task)
    {
        task.Id = _tasks.Count + 1;
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task<List<TaskItem>> GetTasksByUser(string userId)
    {
        return Task.FromResult(_tasks.Where(t => t.OwnerUserId == userId).ToList());
    }

    public Task<List<TaskItem>> GetAllTasks()
    {
        return Task.FromResult(_tasks.ToList());
    }

    public Task<TaskItem?> GetById(int id)
    {
        return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
    }

    public Task Update(TaskItem task)
    {
        return Task.CompletedTask;
    }
}