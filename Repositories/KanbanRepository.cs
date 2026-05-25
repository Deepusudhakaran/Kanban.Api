using Kanban.API.Data;
using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Repositories
{
    public class KanbanRepository(KanbanDbContext dbContext) : IKanbanRepository
    {
        public async Task<IReadOnlyList<KanbanItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Tasks
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<KanbanItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<KanbanItem> CreateAsync(KanbanItem task, CancellationToken cancellationToken)
        {
            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync(cancellationToken);
            return task;
        }

        public async Task UpdateAsync(KanbanItem task, CancellationToken cancellationToken)
        {
            dbContext.Tasks.Update(task);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(KanbanItem task, CancellationToken cancellationToken)
        {
            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
