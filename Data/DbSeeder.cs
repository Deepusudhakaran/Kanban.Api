using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Data
{
    public class DbSeeder
    {
        public static async Task SeedAsync(KanbanDbContext dbContext)
        {
            if (await dbContext.Tasks.AnyAsync())
            {
                return;
            }

            var now = DateTimeOffset.UtcNow;

            dbContext.Tasks.AddRange(
                new KanbanItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Create backend API",
                    Description = "Implement REST endpoints for Kanban tasks.",
                    Status = KanbanItemStatus.InProgress,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new KanbanItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Build Kanban UI",
                    Description = "Create columns and drag/drop behaviour.",
                    Status = KanbanItemStatus.ToDo,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new KanbanItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Prepare README",
                    Description = "Add setup and run instructions.",
                    Status = KanbanItemStatus.Done,
                    CreatedAt = now,
                    UpdatedAt = now
                });

            await dbContext.SaveChangesAsync();
        }
    }
}
