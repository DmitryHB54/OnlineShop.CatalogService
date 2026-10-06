using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Application.Common.Exceptions;

namespace OnlineShop.CatalogService.WebApi.ExceptionHandling;
public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception {Message}", exception.Message);

        ProblemDetails problemDetails = exception switch
        {
            ValidationException validationException => CreateValidationProblem(validationException),
            NotFoundException notFoundException => CreateNotFoundProblem(notFoundException),
            _ => CreateServerErrorProblem()
        };

        httpContext.Response.Clear();
        httpContext.Response.StatusCode = problemDetails.Status ?? 500;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateValidationProblem(ValidationException exception)
    {
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        var problemDetails =  new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "One or more validation errors occured.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        problemDetails.Extensions["errors"] = errors;

        return problemDetails;
    }

    private static ProblemDetails CreateNotFoundProblem(NotFoundException exception)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Resource not found",
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
        };
    }

    private static ProblemDetails CreateServerErrorProblem()
        => new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occured.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
}