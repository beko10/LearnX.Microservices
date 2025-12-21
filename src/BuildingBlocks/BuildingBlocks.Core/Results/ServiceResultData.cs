using Refit;
using System.Net;
using System.Text.Json;

namespace BuildingBlocks.Core.Results;

public class ServiceResult<TData> : ServiceResult, IDataResult<TData>
{
    public TData? Data { get; init; }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    
    public static ServiceResult<TData> SuccessAsOk(TData data)
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.OK,
            Data = data,
        };
    }

    public static ServiceResult<TData> SuccessAsCreated(TData data)
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.Created,
            Data = data,
        };
    }

    // Error Methods
    public static ServiceResult<TData> Fail()
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.BadRequest,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Service operation failed",
            },
        };
    }

    public static ServiceResult<TData> ErrorAsNotFound()
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.NotFound,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "The requested resource was not found.",
            },
        };
    }

    public static ServiceResult<TData> ErrorAsUnauthorized()
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.Unauthorized,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = "Unauthorized access.",
            },
        };
    }

    public static ServiceResult<TData> ErrorAsForbidden()
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.Forbidden,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.Forbidden,
                Title = "Forbidden access.",
            },
        };
    }

    public static ServiceResult<TData> ErrorAsConflict()
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.Conflict,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.Conflict,
                Title = "Conflict occurred.",
            },
        };
    }

    public static ServiceResult<TData> ErrorAsInternalServerError()
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.InternalServerError,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An internal server error occurred.",
            },
        };
    }

    public static ServiceResult<TData> ErrorAsCustom(
        Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails,
        HttpStatusCode statusCode)
    {
        return new ServiceResult<TData>
        {
            StatusCode = statusCode,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = problemDetails.Status ?? (int)statusCode,
                Title = problemDetails.Title,
                Detail = problemDetails.Detail,
                Type = problemDetails.Type,
                Instance = problemDetails.Instance,
                Extensions = problemDetails.Extensions
            },
        };
    }

    public static ServiceResult<TData> ErrorAsCustom(
        HttpStatusCode statusCode,
        string title,
        string? detail = null)
    {
        return new ServiceResult<TData>
        {
            StatusCode = statusCode,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = detail,
            },
        };
    }

    public static ServiceResult<TData> ErrorAsValidation(IDictionary<string, object> errors)
    {
        return new ServiceResult<TData>
        {
            StatusCode = HttpStatusCode.BadRequest,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Validation Errors Occurred",
                Detail = "Please check the errors property for more details",
                Extensions = errors,
            },
        };
    }

    public static ServiceResult<TData> ErrorFromProblemDetails(ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new ServiceResult<TData>
            {
                StatusCode = exception.StatusCode,
                FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Status = (int)exception.StatusCode,
                    Title = exception.Message,
                },
            };
        }

        var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(
            exception.Content,
            JsonOptions);

        return new ServiceResult<TData>
        {
            StatusCode = exception.StatusCode,
            FailProblemDetails = problemDetails!,
        };
    }
    
}