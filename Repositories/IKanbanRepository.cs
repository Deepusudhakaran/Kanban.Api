using Kanban.API.Models;

namespace Kanban.API.Repositories
{
    /// <summary>
    /// Repository contract for low-level data access for KanbanItem entities.
    /// Implementations are responsible for persistence concerns (EF Core, in-memory, etc.).
    /// </summary>
    public interface IKanbanRepository
    {
        /// <summary>
        /// Retrieves all kanban items from the data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>Read-only list of KanbanItem entities.</returns>
        Task<IReadOnlyList<KanbanItem>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a kanban item by identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>The KanbanItem if found; otherwise null.</returns>
        Task<KanbanItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Persists a new kanban item to the data store.
        /// </summary>
        /// <param name="task">The KanbanItem to create.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>The created KanbanItem (including any generated fields such as Id).</returns>
        Task<KanbanItem> CreateAsync(KanbanItem task, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing kanban item in the data store.
        /// </summary>
        /// <param name="task">The KanbanItem with updated values.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        Task UpdateAsync(KanbanItem task, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a kanban item from the data store.
        /// </summary>
        /// <param name="task">The KanbanItem to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        Task DeleteAsync(KanbanItem task, CancellationToken cancellationToken);
    }
}

