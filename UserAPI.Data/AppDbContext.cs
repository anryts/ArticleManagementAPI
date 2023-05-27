using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data.Entities;

namespace UserAPI.Data;

public class AppDbContext : DbContext
{
    public DbSet<Job> Jobs { get; set; } 
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateDateStatistics();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateDateStatistics()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is not UserJob userJob)
                continue;

            userJob.CreatedAt = DateTime.UtcNow;
        }
    }
}