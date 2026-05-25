using Kanban.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Dto;

public sealed record KanbanResponse(
    Guid Id,
    string Title,
    string? Description,
    KanbanItemStatus Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);

public sealed record CreateKanbanRequest(
    [Required, MaxLength(120)] string Title,
    [MaxLength(1000)] string? Description,
    KanbanItemStatus Status = KanbanItemStatus.ToDo);

public sealed record UpdateKanbanRequest(
    [Required, MaxLength(120)] string Title,
    [MaxLength(1000)] string? Description,
    KanbanItemStatus Status);

public sealed record UpdateKanbanStatusRequest(KanbanItemStatus Status);
