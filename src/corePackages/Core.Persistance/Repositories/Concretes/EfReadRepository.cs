using Core.Persistance.Entities;
using Core.Persistance.Paging;
using Core.Persistance.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Drawing;
using System.Linq.Expressions;

namespace Core.Persistance.Repositories.Concretes;

public class EfReadRepository<TEntity, TContext> : IReadRepository<TEntity>
    where TEntity : BaseEntity
    where TContext: DbContext
{
    #region Constructors

    public EfReadRepository(TContext context)
    {
        Context = context;
    }

    #endregion Constructors

    #region Properties

    public DbSet<TEntity> Table => Context.Set<TEntity>();
    protected TContext Context { get; }

    #endregion Properties

    #region Methods

    public int Count() => Table.Count();

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        IQueryable<TEntity> queryable = Table.AsQueryable();
        return await queryable.Where(predicate).CountAsync();
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool tracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Table.AsQueryable();

        if (tracking is false) queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);
        if (predicate is not null) queryable.Where(predicate);
        if (orderBy is not null) orderBy(queryable); else queryable = queryable.OrderByDescending(x => x.CreatedDate);

        return await queryable.Where(x => !x.Deleted && !x.PermentlyDeleted).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IPaginate<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool tracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Table.AsQueryable();
        
        if (tracking is false) queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);
        if (predicate is not null) queryable.Where(predicate);
        if (orderBy is not null) orderBy(queryable); else queryable = queryable.OrderByDescending(x => x.CreatedDate);

        return await queryable.Where(x => !x.Deleted && !x.PermentlyDeleted).ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<IPaginate<TEntity>> GetPaginatedDeletedListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool tracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Table.AsQueryable();

        if (tracking is false) queryable.AsNoTracking();
        if (include is not null) queryable = include(queryable);
        if (predicate is not null) queryable.Where(predicate);
        if (orderBy is not null) orderBy(queryable); else queryable = queryable.OrderByDescending(x => x.CreatedDate);

        return await queryable.Where(x => x.Deleted && !x.PermentlyDeleted).ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
    {
        IQueryable<TEntity> queryable = Table.AsQueryable();
        if (tracking is false) queryable.AsNoTracking();
        return await queryable.FirstAsync(predicate);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
    {
        IQueryable<TEntity> queryable = Table.AsQueryable();
        if (tracking is false) queryable.AsNoTracking();
        return await Table.AnyAsync(predicate);
    }

    public IQueryable<TEntity> Query(bool tracking = true) => Table.AsQueryable();

    #endregion Methods
}
