using BuildingBlocks.Core.Results;
using MediatR;

namespace FileService.API.Features.DeleteFile;

public class DeleteFileRequest : IRequest<DeleteFileResponse>
{
    public string FileName { get; set; } = default!;
}

public class DeleteFileResponse
{
    public ServiceResult Result { get; set; } = default!;
}
