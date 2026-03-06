using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);

        Task<List<TaskItem>> GetTasksByUser(string userId);

        Task<List<TaskItem>> GetAllTasks();

        Task<TaskItem?> GetById(int id);

        Task Update(TaskItem task);
    }
}
