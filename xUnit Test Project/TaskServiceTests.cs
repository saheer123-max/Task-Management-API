using TaskManagement.Application.DTOs;
using Xunit;



public class TaskServiceTests
{
   
    [Fact]
    public async Task CreateTask_ShouldCreateTask()
    {
       
        var repo = new FakeTaskRepository();
        var service = new TaskService(repo);

        var dto = new CreateTaskDto
        {
            Title = "Test Task",
            Description = "Testing task"
        };

       
        await service.CreateTask(dto, "1");

      
        var tasks = await repo.GetTasksByUser("1");

        Assert.Single(tasks);
        Assert.Equal("Test Task", tasks[0].Title);
    }

    
    [Fact]
    public async Task GetUserTasks_ShouldReturnUserTasks()
    {
     
        var repo = new FakeTaskRepository();
        var service = new TaskService(repo);

        await service.CreateTask(new CreateTaskDto
        {
            Title = "Task1",
            Description = "Test"
        }, "1");

        await service.CreateTask(new CreateTaskDto
        {
            Title = "Task2",
            Description = "Test"
        }, "1");

       
        var tasks = await service.GetUserTasks("1");

       
        Assert.Equal(2, tasks.Count);
    }

    // Get All Tasks Test
    [Fact]
    public async Task GetAllTasks_ShouldReturnAllTasks()
    {
        
        var repo = new FakeTaskRepository();
        var service = new TaskService(repo);

        await service.CreateTask(new CreateTaskDto
        {
            Title = "Task1"
        }, "1");

        await service.CreateTask(new CreateTaskDto
        {
            Title = "Task2"
        }, "2");

      
        var tasks = await service.GetAllTasks();

       
        Assert.Equal(2, tasks.Count);
    }

    [Fact]
    public async Task MarkCompleted_ShouldMarkTaskAsCompleted()
    {
        var repo = new FakeTaskRepository();
        var service = new TaskService(repo);

        await service.CreateTask(new CreateTaskDto
        {
            Title = "Task1"
        }, "1");

      
        await service.MarkCompleted(1);

        
        var task = await repo.GetById(1);

        Assert.True(task.IsCompleted);
    }
}