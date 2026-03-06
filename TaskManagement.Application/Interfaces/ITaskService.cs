using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces
{

       public interface ITaskService
    {
        Task CreateTask(CreateTaskDto dto, string userId);

        Task<List<TaskDto>> GetUserTasks(string userId);

        Task<List<TaskDto>> GetAllTasks();

        Task UpdateTask(int id, UpdateTaskDto dto, string userId);

        Task MarkCompleted(int id);
    }
}

