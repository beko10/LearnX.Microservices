using MediatR;

namespace FileService.API.Features.DeleteFile;

public static class DeleteFileEndpoint
{
    public static IEndpointRouteBuilder MapDeleteFileEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v{version:apiVersion}/files/{fileName}", async (
            string fileName,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new DeleteFileRequest { FileName = fileName }, cancellationToken);

            return response.Result.IsSuccess
                ? Results.Ok(response.Result.SuccessMessage)
                : Results.NotFound(response.Result);
        })
        .WithName("DeleteFile")
        .WithTags("Files")
        .WithDescription("Delete a file by name")
        .Produces(200)
        .Produces(404);

        return app;
    }
}
