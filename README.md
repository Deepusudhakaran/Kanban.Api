# Kanban.Api

Kanban.Api is a small RESTful Web API for managing kanban tasks. It exposes endpoints to create, read, update, change status and delete kanban items. The project targets .NET 8.

## Features
- CRUD operations for kanban items
- Update task status via PATCH
- OpenAPI/Swagger support (when running in Development)

## Prerequisites
- .NET 8 SDK (install from https://dotnet.microsoft.com)
- Optional: Visual Studio 2022/2026 or Visual Studio Code

## Build
From the repository root run:

dotnet build

## Run
Using the .NET CLI:

dotnet run --project ./Kanban.API/Kanban.API.csproj

This starts the API using the project's configured launch settings. By default the app will serve Swagger UI in Development environment at /swagger.

Using Visual Studio:

- Open the solution file Kanban.API.slnx
- Set Kanban.API as the startup project and run (F5 or Ctrl+F5)

## API Documentation
When running in Development the OpenAPI/Swagger UI is available at:

http://localhost:{port}/swagger

(Replace {port} with the application port shown in the console or launchSettings.)

	
