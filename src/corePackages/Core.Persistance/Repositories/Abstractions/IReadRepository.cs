using Core.Persistance.Entities;
using Core.Persistance.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistance.Repositories.Abstractions;

public interface IReadRepository<T> : IRepository<T> where T : IEntity
{
    #region Methods

    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                         Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                         int index = 0,
                         int size = 10,
                         bool tracking = true,
                         CancellationToken cancellationToken = default);
    int Count();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
    IQueryable<T> Query(bool tracking = true);

    #endregion Methods
}
