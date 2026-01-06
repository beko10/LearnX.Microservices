using BuildingBlocks.Core.Results;

namespace FileService.API.Common.Rules;

public interface IFileBusinessRules
{
    ServiceResult CheckFileNotEmpty(long fileSize);
    ServiceResult CheckFileSizeIsValid(long fileSize);
    ServiceResult CheckFileExtensionIsAllowed(string fileName);
    ServiceResult CheckFileExists(bool exists);
}
