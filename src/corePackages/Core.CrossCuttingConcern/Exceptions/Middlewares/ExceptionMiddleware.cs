using Core.CrossCuttingConcern.Exceptions.CustomProblemDetails;
using Core.CrossCuttingConcern.Exceptions.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Core.CrossCuttingConcern.Exceptions.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        context.Response.ContentType = "application/json";

        if(exception.GetType() == typeof(BusinessException))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(new BusinessProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "Business",
                Title = "Business exception",
                Detail = exception.Message,
                Instance = "",
            }.ToString());
        }

        if (exception.GetType() == typeof(ValidationException))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            IEnumerable<ValidationFailure> validationErrors = ((ValidationException)exception).Errors;
            return context.Response.WriteAsync(new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "Validation",
                Title = "Validation error(s)",
                Detail = "",
                Instance = "",
                Errors = validationErrors
            }.ToString());

        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(new CustomProblemDetails.CustomProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "Internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }
}
