using FormsApp.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormsApp.Core.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Submission> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.Created).IsRequired();

            // Index on Created column for improved query performance
            // Used by date range filters and OrderByDescending in SearchAsync
            entity.HasIndex(e => e.Created);
        });
    }
}
