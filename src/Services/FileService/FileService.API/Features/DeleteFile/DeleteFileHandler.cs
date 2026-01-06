using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using FileService.API.Common.Rules;
using FileService.API.Common.Services;
using MediatR;

namespace FileService.API.Features.DeleteFile;

public class DeleteFileHandler(
    IFileStorageService fileStorageService,
    IFileBusinessRules businessRules
) : IRequestHandler<DeleteFileRequest, DeleteFileResponse>
{
    public async Task<DeleteFileResponse> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        var fileExists = fileStorageService.FileExists(request.FileName);

        // Business Rules Check
        var businessRulesResult = BusinessRuleEngine.Run(
            businessRules.CheckFileExists(fileExists)
        );

        if (businessRulesResult.IsFail)
        {
            return new DeleteFileResponse { Result = businessRulesResult };
        }

        await fileStorageService.DeleteFileAsync(request.FileName, cancellationToken);

        return new DeleteFileResponse
        {
            Result = ServiceResult.SuccessAsOk("File deleted successfully.")
        };
    }
}
