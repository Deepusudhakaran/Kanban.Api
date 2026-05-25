using Kanban.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.API.Data
{
    public class KanbanDbContext(DbContextOptions<KanbanDbContext> options) : DbContext(options)
    {
        public DbSet<KanbanItem> Tasks => Set<KanbanItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KanbanItem>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(120);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.UpdatedAt).IsRequired();
            });
        }
    }
}
