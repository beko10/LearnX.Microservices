using System.Net;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Results;

public interface IResult
{
    [JsonIgnore] public HttpStatusCode StatusCode { get; init; }
    public Microsoft.AspNetCore.Mvc.ProblemDetails? FailProblemDetails { get; init; }

    [JsonIgnore]
    public bool IsSuccess => FailProblemDetails is null;
}
