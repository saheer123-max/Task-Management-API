using Xunit;
using TaskManagement.Application.DTOs;


public class TaskServiceTests
{
    [Fact]
    public async Task CreateTask_ShouldCreateTask()
    {
        // Arrange
        var repo = new FakeTaskRepository();
        var service = new TaskService(repo);

        var dto = new CreateTaskDto
        {
            Title = "Test Task",
            Description = "Testing task"
        };

        // Act
        await service.CreateTask(dto, "2");

        // Assert
        var tasks = await repo.GetTasksByUser("2");

        Assert.Single(tasks);
        Assert.Equal("Test Task", tasks[0].Title);
    }
}