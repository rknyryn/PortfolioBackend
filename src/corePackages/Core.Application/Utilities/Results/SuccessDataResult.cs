namespace Core.Application.Utilities.Results;

public class SuccessDataResult<T> : SuccessResult, IDataResult<T>
{
    #region Constructors

    public SuccessDataResult(T data, string message) : base(message)
    {
        Data = data;
    }

    public SuccessDataResult(T data)
    {
        Data = data;
    }

    #endregion Constructors

    #region Properties

    public T Data { get; }

    #endregion Properties
}
