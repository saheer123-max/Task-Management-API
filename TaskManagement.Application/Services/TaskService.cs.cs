using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

using TaskManagement.Domain.Interfaces;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
    }

    // Create Task
    public async Task CreateTask(CreateTaskDto dto, string userId)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false,
            OwnerUserId = userId
        };

        await _repo.AddAsync(task);
    }

    // Get tasks for logged user
    public async Task<List<TaskDto>> GetUserTasks(string userId)
    {
        var tasks = await _repo.GetTasksByUser(userId);

        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt,
            DueDate = t.DueDate
        }).ToList();
    }

    // Get all tasks (Admin)
    public async Task<List<TaskDto>> GetAllTasks()
    {
        var tasks = await _repo.GetAllTasks();

        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt,
            DueDate = t.DueDate
        }).ToList();
    }

    // Update task (only owner allowed)
    public async Task UpdateTask(int id, UpdateTaskDto dto, string userId)
    {
        var task = await _repo.GetById(id);

        if (task == null)
            throw new Exception("Task not found");

        if (task.OwnerUserId != userId)
            throw new Exception("Unauthorized");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;

        await _repo.Update(task);
    }

    // Admin mark task completed
    public async Task MarkCompleted(int id)
    {
        var task = await _repo.GetById(id);

        if (task == null)
            throw new Exception("Task not found");

        task.IsCompleted = true;

        await _repo.Update(task);
    }
}