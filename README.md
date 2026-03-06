# Task Management API

This is a simple Task Management Web API built with ASP.NET Core following Clean Architecture principles.

## Features

- Create Task
- Update Task
- View Tasks
- Admin can view all tasks
- Admin can mark tasks as completed

## Technologies

- ASP.NET Core Web API
- Entity Framework Core (InMemory)
- xUnit Unit Testing
- Clean Architecture

## Test Users

Admin

UserId: 1  
Role: Admin  

User

UserId: 2  
Role: User  

## API Endpoints

POST /api/tasks  
GET /api/tasks  
PUT /api/tasks/{id}  
PUT /api/tasks/{id}/complete  

## Running Tests

Open **Test Explorer** and click **Run All Tests**.