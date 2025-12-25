using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;
using FluentValidation;
using MongoDB.Bson;

namespace CatalogService.Application.Features.CategoryFeature.Validators;

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
            .Must(BeAValidObjectId)
            .WithMessage(CategoryValidationMessages.IdInvalid)
            .WithErrorCode(CategoryValidationMessages.IdInvalidCode);
    }

    private bool BeAValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}
