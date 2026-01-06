using BuildingBlocks.Core.Results;
using MediatR;

namespace FileService.API.Features.UploadFile;

public class UploadFileRequest : IRequest<UploadFileResponse>
{
    public IFormFile File { get; set; } = default!;
}

public class UploadFileResponse
{
    public ServiceResult<UploadFileResultDto> Result { get; set; } = default!;
}

public class UploadFileResultDto
{
    public string FileName { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = default!;
}
