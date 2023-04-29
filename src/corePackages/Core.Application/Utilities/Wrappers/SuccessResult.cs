namespace Core.Application.Utilities.Wrappers;

public class SuccessResult : IMessageResult
{
    #region Constructors

    public SuccessResult()
    {

    }
    public SuccessResult(string? message)
    {
        Message = message;
    }

    #endregion Constructors

    #region Properties

    public string? Message { get; }

    #endregion Properties
}
