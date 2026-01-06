using System.ComponentModel.DataAnnotations;

namespace FileService.API.Common.Options;

public class FileUploadOptions
{
    public const string SectionName = "FileOptions";

    [Required]
    [Range(1, 100)]
    public int MaxFileSizeMB { get; set; } = 10;

    [Required]
    public string[] AllowedExtensions { get; set; } = [".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx"];

    [Required]
    public string UploadPath { get; set; } = "wwwroot/files";

    public long MaxFileSizeBytes => MaxFileSizeMB * 1024 * 1024;
}
