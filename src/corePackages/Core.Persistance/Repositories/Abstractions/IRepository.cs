using Core.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistance.Repositories.Abstractions;

public interface IRepository<T> where T : IEntity
{
    #region Properties

    public DbSet<T> Table { get; }

    #endregion Properties
}
