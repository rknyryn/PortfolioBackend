namespace Core.Application.Utilities.Wrappers;

public interface IDataResult<out T> : IResult
{
    #region Properties

    T Data { get; }

    #endregion Properties
}
