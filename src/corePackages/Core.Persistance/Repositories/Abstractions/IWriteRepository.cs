using Core.Persistance.Entities;

namespace Core.Persistance.Repositories.Abstractions;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    #region Methods

    T Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Update(T entity);

    void Delete(T entity);

    void DeleteRange(IEnumerable<T> entities);

    Task DeleteByIdAsync(Guid id);

    Task DeleteByIdsAsync(IEnumerable<Guid> ids);

    #endregion Methods
}
