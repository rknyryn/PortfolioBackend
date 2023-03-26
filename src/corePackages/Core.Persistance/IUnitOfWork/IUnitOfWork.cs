namespace Core.Persistance.IUnitOfWork;

public interface IUnitOfWork
{
    #region Methods

    Task<bool> SaveChangesAsync();

    #endregion Methods
}
