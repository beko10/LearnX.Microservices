using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CourseFeature.Queries.GetByIdCourseQuery;
using FluentValidation;

namespace CatalogService.Application.Features.CourseFeature.Validatiors;

public class GetByIdCourseQueryRequestValidator : AbstractValidator<GetByIdCourseQueryRequest>
{
    public GetByIdCourseQueryRequestValidator()
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

