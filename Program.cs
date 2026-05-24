using Kanban.API.Data;
using Kanban.API.Middleware;
using Kanban.API.Repositories;
using Kanban.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var databaseProvider = builder.Configuration["DatabaseProvider"] ?? "InMemory";

builder.Services.AddDbContext<KanbanDbContext>(options =>
{
    if (databaseProvider.Equals("SQLite", StringComparison.OrdinalIgnoreCase))
    {
        //options.UseSqlite(builder.Configuration.GetConnectionString("KanbanDb"));
    }
    else
    {
        options.UseInMemoryDatabase("KanbanDb");
    }
});

builder.Services.AddScoped<IKanbanRepository, KanbanRepository>();
builder.Services.AddScoped<IKanbanService, KanbanService>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Frontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<KanbanDbContext>();
    if (databaseProvider.Equals("SQLite", StringComparison.OrdinalIgnoreCase))
    {
        await db.Database.MigrateAsync();
    }
    await DbSeeder.SeedAsync(db);
}

await app.RunAsync();
