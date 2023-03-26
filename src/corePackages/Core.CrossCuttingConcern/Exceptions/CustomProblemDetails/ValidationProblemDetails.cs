namespace Core.CrossCuttingConcern.Exceptions.CustomProblemDetails;

public class ValidationProblemDetails : CustomProblemDetails
{
    #region Properties

    public object Errors { get; set; }

    #endregion Properties
}
