using Kanban.API.Dto;

namespace Kanban.API.Services
{
    public interface IKanbanService
    {
        Task<IReadOnlyList<KanbanResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<KanbanResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<KanbanResponse> CreateAsync(CreateKanbanRequest request, CancellationToken cancellationToken);
        Task<KanbanResponse?> UpdateAsync(Guid id, UpdateKanbanRequest request, CancellationToken cancellationToken);
        Task<KanbanResponse?> UpdateStatusAsync(Guid id, UpdateKanbanStatusRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
