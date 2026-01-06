namespace FileService.API.Common.Services;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken = default);
    bool FileExists(string fileName);
    string GetFilePath(string fileName);
}
