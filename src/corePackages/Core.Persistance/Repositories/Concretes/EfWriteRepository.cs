using Core.Persistance.Entities;
using Core.Persistance.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistance.Repositories.Concretes;

public class EfWriteRepository<TEntity, TContext> : IWriteRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    #region Constructors

    public EfWriteRepository(TContext context)
    {
        Context = context;
    }

    #endregion Constructors

    #region Properties

    public DbSet<TEntity> Table => Context.Set<TEntity>();
    protected TContext Context { get; }

    #endregion Properties

    #region Methods

    public TEntity Add(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Added;
        return entity;
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            Add(entity);
        }
    }

    public void Delete(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        TEntity entity = await Table.FirstAsync(predicate: p => p.Id == id);
        Delete(entity);
    }

    public async Task DeleteByIdsAsync(IEnumerable<Guid> ids)
    {
        IEnumerable<TEntity> entities = await Table.Where(predicate: p => ids.Contains(p.Id)).ToListAsync();
        foreach (TEntity entity in entities)
        {
            Delete(entity);
        }
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            Delete(entity);
        }
    }

    public void Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    #endregion Methods
}
