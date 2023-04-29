namespace Core.Application.Utilities.Wrappers;

public interface IDataResult<out T> : IMessageResult
{
    #region Properties

    T Data { get; }

    #endregion Properties
}
