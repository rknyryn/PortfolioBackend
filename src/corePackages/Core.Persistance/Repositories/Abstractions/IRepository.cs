using Core.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistance.Repositories.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    #region Properties

    public DbSet<T> Table { get; }

    #endregion Properties
}
