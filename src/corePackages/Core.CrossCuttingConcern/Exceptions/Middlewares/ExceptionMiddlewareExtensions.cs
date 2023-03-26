using Microsoft.AspNetCore.Builder;

namespace Core.CrossCuttingConcern.Exceptions.Middlewares;

public static class ExceptionMiddlewareExtensions
{
    #region Methods

    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }

    #endregion Methods
}
