using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Results;

public class ServiceResult : IResult
{
    [JsonIgnore] public HttpStatusCode StatusCode { get; init; }
    public Microsoft.AspNetCore.Mvc.ProblemDetails? FailProblemDetails { get; init; }

    [JsonIgnore]
    public bool IsSuccess => FailProblemDetails is null;

    [JsonIgnore]
    public bool IsFail => !IsSuccess;

    public string SuccessMessage { get; init; } = string.Empty;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    // Localization için mesaj dictionary'si
    private static Dictionary<string, string> _errorMessages = new()
    {
        ["ServiceOperationFailed"] = "Service operation failed",
        ["NotFound"] = "The requested resource was not found.",
        ["Unauthorized"] = "Unauthorized access.",
        ["Forbidden"] = "Forbidden access.",
        ["Conflict"] = "Conflict occurred.",
        ["InternalServerError"] = "An internal server error occurred.",
        ["ValidationError"] = "Validation Error"
    };

    public static void ConfigureErrorMessages(Dictionary<string, string> messages)
    {
        _errorMessages = messages;
    }

    // Success Methods
    public static ServiceResult SuccessAsNoContent()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.NoContent,
        };
    }

    public static ServiceResult SuccessAsOk()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.OK,
        };
    }

    public static ServiceResult SuccessAsOk(string messages)
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.OK,
            SuccessMessage = messages
        };
    }


    public static ServiceResult SuccessAsCreated()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.Created,
        };
    }

    // Error Methods
    public static ServiceResult Fail()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = _errorMessages["ServiceOperationFailed"],
            },
        };
    }

    public static ServiceResult Fail(string detail)
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = _errorMessages["ServiceOperationFailed"],
                Detail = detail,
            },
        };
    }

    public static ServiceResult Fail(HttpStatusCode httpStatusCode,string detail)
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)httpStatusCode,
                Title = _errorMessages["ServiceOperationFailed"],
                Detail = detail,
            },
        };
    }

    public static ServiceResult ErrorAsNotFound()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.NotFound,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = _errorMessages["NotFound"],
            },
        };
    }

    public static ServiceResult ErrorAsNotFound(string detail)
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.NotFound,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = _errorMessages["NotFound"],
                Detail = detail,
            },
        };
    }

    public static ServiceResult ErrorAsUnauthorized()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.Unauthorized,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = _errorMessages["Unauthorized"],
            },
        };
    }

    public static ServiceResult ErrorAsForbidden()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.Forbidden,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.Forbidden,
                Title = _errorMessages["Forbidden"],
            },
        };
    }

    public static ServiceResult ErrorAsConflict()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.Conflict,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.Conflict,
                Title = _errorMessages["Conflict"],
            },
        };
    }

    public static ServiceResult ErrorAsInternalServerError()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.InternalServerError,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = _errorMessages["InternalServerError"],
            },
        };
    }

    public static ServiceResult ErrorAsCustom(Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        return new ServiceResult
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

    public static ServiceResult ErrorAsCustom(
        HttpStatusCode statusCode,
        string title,
        string? detail = null)
    {
        return new ServiceResult
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

    public static ServiceResult ErrorAsValidation(IDictionary<string, object> errors)
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            FailProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = _errorMessages["ValidationError"],
                Extensions = errors,
            },
        };
    }

    public static ServiceResult ErrorFromProblemDetails(ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new ServiceResult
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

        return new ServiceResult
        {
            StatusCode = exception.StatusCode,
            FailProblemDetails = problemDetails!,
        };
    }
}