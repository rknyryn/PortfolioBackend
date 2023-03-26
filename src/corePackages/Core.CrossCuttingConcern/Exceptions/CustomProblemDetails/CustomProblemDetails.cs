using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcern.Exceptions.CustomProblemDetails;

public class CustomProblemDetails : ProblemDetails
{
    #region Methods

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    #endregion Methods
}
