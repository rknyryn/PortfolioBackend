namespace Core.Application.Utilities.Results;

public interface IDataResult<out T> : IResult
{
    #region Properties

    T Data { get; }

    #endregion Properties
}
