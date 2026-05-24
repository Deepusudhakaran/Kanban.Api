using Kanban.API.Models;

namespace Kanban.API.Repositories
{
    public interface IKanbanRepository
    {
        Task<IReadOnlyList<KanbanItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<KanbanItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<KanbanItem> CreateAsync(KanbanItem task, CancellationToken cancellationToken);
        Task UpdateAsync(KanbanItem task, CancellationToken cancellationToken);
        Task DeleteAsync(KanbanItem task, CancellationToken cancellationToken);
    }
}

