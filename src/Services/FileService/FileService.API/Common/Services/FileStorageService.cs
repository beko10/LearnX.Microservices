using FileService.API.Common.Options;
using Microsoft.Extensions.Options;

namespace FileService.API.Common.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _uploadPath;

    public FileStorageService(IOptions<FileUploadOptions> options, IWebHostEnvironment environment)
    {
        _uploadPath = Path.Combine(environment.ContentRootPath, options.Value.UploadPath);
        
        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var uniqueFileName = $"{Guid.NewGuid():N}_{fileName}";
        var filePath = Path.Combine(_uploadPath, uniqueFileName);

        await using var outputStream = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(outputStream, cancellationToken);

        return $"/files/{uniqueFileName}";
    }

    public Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_uploadPath, fileName);

        if (!File.Exists(filePath))
        {
            return Task.FromResult(false);
        }

        File.Delete(filePath);
        return Task.FromResult(true);
    }

    public bool FileExists(string fileName)
    {
        var filePath = Path.Combine(_uploadPath, fileName);
        return File.Exists(filePath);
    }

    public string GetFilePath(string fileName)
    {
        return $"/files/{fileName}";
    }
}
