using Core.Persistance.Entities;
using Core.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Portfolio.Persistance.Contexts;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    #region Constructors

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #endregion Constructors

    #region Methods

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<BaseEntity>();
        foreach (var entity in datas)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedDate = DateTime.UtcNow;
                entity.Entity.UpdatedDate = DateTime.UtcNow;
                entity.Entity.Deleted = false;
                entity.Entity.PermentlyDeleted = false;
            }
            else if (entity.State == EntityState.Modified)
            {
                entity.Entity.UpdatedDate = DateTime.UtcNow;
            }
            else if(entity.State == EntityState.Deleted)
            {
                entity.Entity.Deleted = true;
                entity.Entity.DeletedDate = DateTime.UtcNow;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    #endregion Methods
}
