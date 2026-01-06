using MediatR;

namespace FileService.API.Features.UploadFile;

public static class UploadFileEndpoint
{
    public static IEndpointRouteBuilder MapUploadFileEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v{version:apiVersion}/files", async (
            IFormFile file,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new UploadFileRequest { File = file }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Created(response.Result.Data!.FilePath, response.Result.Data)
                : Results.BadRequest(response.Result);
        })
        .WithName("UploadFile")
        .WithTags("Files")
        .WithDescription("Upload a file and get its path")
        .DisableAntiforgery()
        .Produces<UploadFileResultDto>(201)
        .Produces(400);

        return app;
    }
}
