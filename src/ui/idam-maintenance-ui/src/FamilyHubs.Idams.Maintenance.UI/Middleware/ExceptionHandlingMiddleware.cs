using System.Diagnostics.CodeAnalysis;
using FamilyHubs.Idams.Maintenance.Core.Exceptions;
using FamilyHubs.SharedKernel.Exceptions;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.Json;
using ValidationException = FluentValidation.ValidationException;

namespace FamilyHubs.Idams.Maintenance.UI.Middleware;

[ExcludeFromCodeCoverage]
internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Api Middleware");
            await HandleExceptionAsync(context, e);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new ApiExceptionResponse
        {
            Title = GetTitle(exception),
            StatusCode = statusCode,
            Detail = exception.Message,
            Errors = GetValidationErrors(exception),
            ErrorCode = GetErrorCode(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception)
    {
        if (exception is IdamsException idamsException)
        {
            return idamsException.HttpStatusCode;
        }

        return exception.GetType().ShortDisplayName() switch
        {
            "BadRequestException" => StatusCodes.Status400BadRequest,
            "NotFoundException" => StatusCodes.Status404NotFound,
            "ValidationException" => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitle(Exception exception)
    {
        return exception switch
        {
            IdamsException idamsException => idamsException.Title,
            ApplicationException applicationException => applicationException.Message,
            _ => "Server Error"
        };
    }


    private static IEnumerable<ValidationFailure> GetValidationErrors(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return validationException.Errors;
        }
        return new List<ValidationFailure>();
    }

    private static string GetErrorCode(Exception exception)
    {
        if (exception is IdamsException idamsException)
        {
            return idamsException.ErrorCode;
        }

        return ExceptionCodes.UnhandledException;
    }
}