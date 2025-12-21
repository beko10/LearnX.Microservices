using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CourseFeature.Commands.DeleteCourseCommand;
using FluentValidation;

namespace CatalogService.Application.Features.CourseFeature.Validatiors;

public class DeleteCourseCommandRequestValidator : AbstractValidator<DeleteCourseCommandRequest>
{
    public DeleteCourseCommandRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.IdRequired)
            .WithErrorCode(CourseValidationMessages.IdRequiredCode)
            .NotNull()
            .WithMessage(CourseValidationMessages.IdNull)
            .WithErrorCode(CourseValidationMessages.IdNullCode)
            .Must(BeAValidGuid)
            .WithMessage(CourseValidationMessages.IdInvalid)
            .WithErrorCode(CourseValidationMessages.IdInvalidCode);
    }

    private bool BeAValidGuid(Guid id)
    {
        return id != Guid.Empty;
    }
}

