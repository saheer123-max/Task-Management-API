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

    private string UserId => Request.Headers["UserId"];
    private string Role => Request.Headers["Role"];

    [HttpPost]
    public async Task<IActionResult> Create(
      [FromHeader] string UserId,
      CreateTaskDto dto)
    {
        await _service.CreateTask(dto, UserId);
        return Ok("Task created successfully");
    }
    [HttpGet]
    public async Task<IActionResult> Get(
     [FromHeader] string UserId,
     [FromHeader] string Role)
    {
        if (Role == "Admin")
            return Ok(await _service.GetAllTasks());

        return Ok(await _service.GetUserTasks(UserId));
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
      int id,
      UpdateTaskDto dto,
      [FromHeader] string UserId)
    {
        await _service.UpdateTask(id, dto, UserId);
        return Ok("Updated");
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(
      int id,
      [FromHeader] string Role)
    {
        if (Role != "Admin")
            return Unauthorized();

        await _service.MarkCompleted(id);

        return Ok("Marked as completed");
    }
}