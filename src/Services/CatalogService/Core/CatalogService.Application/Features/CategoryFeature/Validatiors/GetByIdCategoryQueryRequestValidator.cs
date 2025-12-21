using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;
using FluentValidation;

namespace CatalogService.Application.Features.CategoryFeature.Validatiors;

public class GetByIdCategoryQueryRequestValidator : AbstractValidator<GetByIdCategoryQueryRequest>
{
    public GetByIdCategoryQueryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(CategoryValidationMessages.IdRequired)
            .WithErrorCode(CategoryValidationMessages.IdRequiredCode)
            .NotNull()
            .WithMessage(CategoryValidationMessages.IdNull)
            .WithErrorCode(CategoryValidationMessages.IdNullCode)
            .Must(BeAValidGuid)
            .WithMessage(CategoryValidationMessages.IdInvalid)
            .WithErrorCode(CategoryValidationMessages.IdInvalidCode);
    }

    private bool BeAValidGuid(Guid id)
    {
        return id != Guid.Empty;
    }
}
