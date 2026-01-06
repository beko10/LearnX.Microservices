using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using FileService.API.Common.Rules;
using FileService.API.Common.Services;
using MediatR;

namespace FileService.API.Features.UploadFile;

public class UploadFileHandler(
    IFileStorageService fileStorageService,
    IFileBusinessRules businessRules
) : IRequestHandler<UploadFileRequest, UploadFileResponse>
{
    public async Task<UploadFileResponse> Handle(UploadFileRequest request, CancellationToken cancellationToken)
    {
        var file = request.File;

        // Business Rules Check
        var businessRulesResult = BusinessRuleEngine.Run(
            businessRules.CheckFileNotEmpty(file.Length),
            businessRules.CheckFileSizeIsValid(file.Length),
            businessRules.CheckFileExtensionIsAllowed(file.FileName)
        );

        if (businessRulesResult.IsFail)
        {
            return new UploadFileResponse
            {
                Result = ServiceResult<UploadFileResultDto>.ErrorAsCustom(
                    businessRulesResult.StatusCode,
                    businessRulesResult.FailProblemDetails?.Title ?? "Validation Error",
                    businessRulesResult.FailProblemDetails?.Detail)
            };
        }

        await using var stream = file.OpenReadStream();
        var filePath = await fileStorageService.SaveFileAsync(stream, file.FileName, cancellationToken);

        return new UploadFileResponse
        {
            Result = ServiceResult<UploadFileResultDto>.SuccessAsCreated(new UploadFileResultDto
            {
                FileName = Path.GetFileName(filePath),
                FilePath = filePath,
                FileSize = file.Length,
                ContentType = file.ContentType
            })
        };
    }
}
