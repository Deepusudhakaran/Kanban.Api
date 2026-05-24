using Kanban.API.Dto;
using Kanban.API.Models;
using Kanban.API.Repositories;

namespace Kanban.API.Services
{
    public class KanbanService(IKanbanRepository kanbanRepository) : IKanbanService
    {
        public async Task<IReadOnlyList<KanbanResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var tasks = await kanbanRepository.GetAllAsync(cancellationToken);
            return tasks.Select(MapToResponse).ToList();
        }

        public async Task<KanbanResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await kanbanRepository.GetByIdAsync(id, cancellationToken);
            return task is null ? null : MapToResponse(task);
        }

        public async Task<KanbanResponse> CreateAsync(CreateKanbanRequest request, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow;
            var task = new KanbanItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title.Trim(),
                Description = request.Description?.Trim(),
                Status = request.Status,
                CreatedAt = now,
                UpdatedAt = now
            };

            var created = await kanbanRepository.CreateAsync(task, cancellationToken);
            return MapToResponse(created);
        }

        public async Task<KanbanResponse?> UpdateAsync(Guid id, UpdateKanbanRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanRepository.GetByIdAsync(id, cancellationToken);
            if (task is null)
            {
                return null;
            }

            task.Title = request.Title.Trim();
            task.Description = request.Description?.Trim();
            task.Status = request.Status;
            task.UpdatedAt = DateTimeOffset.UtcNow;

            await kanbanRepository.UpdateAsync(task, cancellationToken);
            return MapToResponse(task);
        }

        public async Task<KanbanResponse?> UpdateStatusAsync(Guid id, UpdateKanbanStatusRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanRepository.GetByIdAsync(id, cancellationToken);
            if (task is null)
            {
                return null;
            }

            task.Status = request.Status;
            task.UpdatedAt = DateTimeOffset.UtcNow;

            await kanbanRepository.UpdateAsync(task, cancellationToken);
            return MapToResponse(task);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await kanbanRepository.GetByIdAsync(id, cancellationToken);
            if (task is null)
            {
                return false;
            }

            await kanbanRepository.DeleteAsync(task, cancellationToken);
            return true;
        }

        private static KanbanResponse MapToResponse(KanbanItem task) => new(
            task.Id,
            task.Title,
            task.Description,
            task.Status,
            task.CreatedAt,
            task.UpdatedAt);
    }
}
