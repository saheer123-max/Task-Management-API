using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    // values coming from middleware
    private string UserId => HttpContext.Items["UserId"]?.ToString();
    private string Role => HttpContext.Items["Role"]?.ToString();

    // Create Task
    [HttpPost]
    public async Task<IActionResult> Create(
     [FromHeader] string UserId,
     [FromHeader] string Role,
     CreateTaskDto dto)
    {
        await _service.CreateTask(dto, UserId);
        return Ok("Task created successfully");
    }

    // Get Tasks
    [HttpGet]
    public async Task<IActionResult> Get(
      [FromHeader] string UserId,
      [FromHeader] string Role)
    {
        if (Role == "Admin")
            return Ok(await _service.GetAllTasks());

        return Ok(await _service.GetUserTasks(UserId));
    }

    // Update Task
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
      int id,
      [FromHeader] string UserId,
      [FromHeader] string Role,
      UpdateTaskDto dto)
    {
        await _service.UpdateTask(id, dto, UserId);
        return Ok("Task updated successfully");
    }

    // Mark Completed (Admin only)
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(
       int id,
       [FromHeader] string UserId,
       [FromHeader] string Role)
    {
        if (Role != "Admin")
            return Unauthorized("Only admin can complete tasks");

        await _service.MarkCompleted(id);

        return Ok("Task marked as completed");
    }
}