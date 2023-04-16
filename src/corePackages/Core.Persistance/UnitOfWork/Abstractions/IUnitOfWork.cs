namespace Core.Persistance.UnitOfWork.Abstractions;

public interface IUnitOfWork
{
    #region Methods

    Task<int> SaveChangesAsync();

    #endregion Methods
}
