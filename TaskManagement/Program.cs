using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;

using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;

using TaskManagement.API.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskDb"));



builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();


var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseMiddleware<FakeAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();