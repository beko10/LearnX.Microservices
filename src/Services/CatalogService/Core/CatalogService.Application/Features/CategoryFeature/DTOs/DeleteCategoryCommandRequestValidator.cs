
using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;
using FluentValidation;

namespace CatalogService.Application.Features.CategoryFeature.DTOs;

public class DeleteCategoryCommandRequestValidator : AbstractValidator<DeleteCategoryCommandRequest>
{
    public DeleteCategoryCommandRequestValidator()
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
