using Kanban.API.Dto;

namespace Kanban.API.Services
{
    /// <summary>
    /// Service contract for managing Kanban tasks.
    /// Implementations perform data access and business logic for CRUD operations and status changes.
    /// </summary>
    public interface IKanbanService
    {
        /// <summary>
        /// Retrieves all kanban tasks.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>Read-only list of KanbanResponse objects.</returns>
        Task<IReadOnlyList<KanbanResponse>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a single kanban task by id.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>The KanbanResponse if found; otherwise null.</returns>
        Task<KanbanResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new kanban task.
        /// </summary>
        /// <param name="request">Payload containing new task details.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>The created KanbanResponse.</returns>
        Task<KanbanResponse> CreateAsync(CreateKanbanRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing kanban task.
        /// </summary>
        /// <param name="id">Identifier of the task to update.</param>
        /// <param name="request">Update payload containing new values.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>The updated KanbanResponse if the task exists; otherwise null.</returns>
        Task<KanbanResponse?> UpdateAsync(Guid id, UpdateKanbanRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Updates only the status field of an existing task.
        /// </summary>
        /// <param name="id">Identifier of the task whose status will be changed.</param>
        /// <param name="request">Payload containing the new status value.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>The updated KanbanResponse if the task exists; otherwise null.</returns>
        Task<KanbanResponse?> UpdateStatusAsync(Guid id, UpdateKanbanStatusRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a kanban task by id.
        /// </summary>
        /// <param name="id">Identifier of the task to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>True if deletion succeeded; otherwise false.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
