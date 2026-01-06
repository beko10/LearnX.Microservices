using FluentValidation;

namespace FileService.API.Features.DeleteFile;

public class DeleteFileValidator : AbstractValidator<DeleteFileRequest>
{
    public DeleteFileValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("File name is required.")
            .NotNull().WithMessage("File name cannot be null.");
    }
}
