using Domain.Abstractions;
using Domain.Entities.Identity;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class MainDbContext : IdentityDbContext<User, Role, int>, IUnitOfWork
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddCustomIdentityMappings();

    }

    public override int SaveChanges()
    {
        _softDeleter();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _softDeleter();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void _softDeleter()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeleteEntity)
            {
                entry.State = EntityState.Modified;
                var entity = (ISoftDeleteEntity)entry.Entity;
                entity.IsDeleted = true;
                entity.DeletedDateTime = DateTime.Now;
            }
        }
    }
}