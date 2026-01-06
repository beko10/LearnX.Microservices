using FluentValidation;

namespace FileService.API.Features.UploadFile;

public class UploadFileValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.");
    }
}
