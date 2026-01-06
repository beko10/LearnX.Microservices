using BuildingBlocks.Core.Results;
using FileService.API.Common.Options;
using Microsoft.Extensions.Options;

namespace FileService.API.Common.Rules;

public class FileBusinessRules(IOptions<FileUploadOptions> options) : IFileBusinessRules
{
    private readonly FileUploadOptions _options = options.Value;

    public ServiceResult CheckFileNotEmpty(long fileSize)
    {
        return fileSize > 0
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail("File is empty.");
    }

    public ServiceResult CheckFileSizeIsValid(long fileSize)
    {
        return fileSize <= _options.MaxFileSizeBytes
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail($"File size exceeds maximum allowed size of {_options.MaxFileSizeMB}MB.");
    }

    public ServiceResult CheckFileExtensionIsAllowed(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        
        return _options.AllowedExtensions.Contains(extension)
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail($"File extension '{extension}' is not allowed. Allowed: {string.Join(", ", _options.AllowedExtensions)}");
    }

    public ServiceResult CheckFileExists(bool exists)
    {
        return exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.ErrorAsNotFound("File not found.");
    }
}
