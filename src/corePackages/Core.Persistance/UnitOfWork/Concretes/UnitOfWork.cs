using Core.Persistance.UnitOfWork.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistance.UnitOfWork.Concretes;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    #region Constructors

    public UnitOfWork(TContext context)
    {
        Context = context;
    }

    #endregion Constructors

    #region Properties

    protected TContext Context { get; }

    #endregion Properties

    #region Methods

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }

    #endregion Methods
}
