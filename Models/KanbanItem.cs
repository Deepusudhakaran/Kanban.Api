using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Models
{
    public class KanbanItem
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public KanbanItemStatus Status { get; set; } = KanbanItemStatus.ToDo;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
