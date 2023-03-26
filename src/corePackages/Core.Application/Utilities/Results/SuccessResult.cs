namespace Core.Application.Utilities.Results;

public class SuccessResult : IResult
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
