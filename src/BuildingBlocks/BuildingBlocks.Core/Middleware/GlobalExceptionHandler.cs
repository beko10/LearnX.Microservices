using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Core.Exceptions;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IWebHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // 1. Exception Tipine Göre Detayları Belirle (Pattern Matching)
        (int statusCode, string title, string detail, string errorCode, IEnumerable<string>? errors) details = exception switch
        {
            ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                "Validation Failed",
                "One or more validation errors occurred.",
                "VALIDATION_FAILED",
                validationEx.Errors.Select(e => e.ErrorMessage)
            ),

            // Custom Exceptions (Senin projendeki namespace'leri buraya eklemelisin)
           //NotFounxception notFoundEx => (404, "Not Found", notFoundEx.Message, "ENTITY_NOT_FOUND", null),
            // BusinessRuleException busEx => (422, "Business Rule Violation", busEx.Message, "BUSINESS_RULE_VIOLATION", null),

            KeyNotFoundException keyEx => (
                StatusCodes.Status404NotFound,
                "Resource Not Found",
                keyEx.Message,
                "ENTITY_NOT_FOUND",
                null
            ),

            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                "Access denied.",
                "UNAUTHORIZED",
                null
            ),

            ArgumentException argEx => (
                StatusCodes.Status400BadRequest,
                "Invalid Argument",
                argEx.Message,
                "INVALID_ARGUMENT",
                null
            ),

            TimeoutException => (
                StatusCodes.Status408RequestTimeout,
                "Timeout",
                "The operation has timed out.",
                "OPERATION_TIMEOUT",
                null
            ),

            NotImplementedException => (
                StatusCodes.Status501NotImplemented,
                "Not Implemented",
                "Feature not implemented.",
                "NOT_IMPLEMENTED",
                null
            ),

            // Default / Unhandled
            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                environment.IsDevelopment() ? exception.Message : "An internal server error occurred.",
                "INTERNAL_SERVER_ERROR",
                null
            )
        };


        LogException(httpContext, exception, details.statusCode, details.errorCode);

   
        var problemDetails = new ProblemDetails
        {
            Status = details.statusCode,
            Title = details.title,
            Detail = details.detail,
            Instance = httpContext.Request.Path,
            Type = $"https://httpstatuses.com/{details.statusCode}"
        };

        // Senin özel alanlarını 'Extensions' dictionary'sine ekliyoruz.
        // Bu standart bir yöntemdir ve JSON'a direkt property olarak yansır.
        problemDetails.Extensions.Add("errorCode", details.errorCode);
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        problemDetails.Extensions.Add("timestamp", DateTime.UtcNow);

        if (details.errors is not null)
        {
            problemDetails.Extensions.Add("errors", details.errors);
        }

        // 4. Yanıtı Yazma
        httpContext.Response.StatusCode = details.statusCode;
        // Problem+json standart content type'dır
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private void LogException(HttpContext context, Exception exception, int statusCode, string errorCode)
    {
        var logLevel = statusCode switch
        {
            >= 500 => LogLevel.Error,
            >= 400 => LogLevel.Warning,
            _ => LogLevel.Information
        };

        logger.Log(logLevel, exception,
            "[{ErrorCode}] {ExceptionType} handled at {Path}. Status: {StatusCode}. TraceId: {TraceId}",
            errorCode,
            exception.GetType().Name,
            context.Request.Path,
            statusCode,
            context.TraceIdentifier);
    }
}