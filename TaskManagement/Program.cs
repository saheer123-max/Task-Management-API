using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;

using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;

using TaskManagement.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskDb"));


// Dependency Injection
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();


var app = builder.Build();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Custom Middleware
app.UseMiddleware<FakeAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();