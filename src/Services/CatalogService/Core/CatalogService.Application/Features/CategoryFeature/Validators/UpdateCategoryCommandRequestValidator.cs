using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;
using CatalogService.Application.Features.CategoryFeature.DTOs;
using FluentValidation;
using MongoDB.Bson;

namespace CatalogService.Application.Features.CategoryFeature.Validators;

public class UpdateCategoryCommandRequestValidator : AbstractValidator<UpdateCategoryCommandRequest>
{
    public UpdateCategoryCommandRequestValidator()
    {
        RuleFor(x => x.UpdateCategoryCommandRequestDto!.Name)
            .NotEmpty()
            .WithMessage(CategoryValidationMessages.NameRequired)
            .WithErrorCode(CategoryValidationMessages.NameRequiredCode)
            .NotNull()
            .WithMessage(CategoryValidationMessages.NameNull)
            .WithErrorCode(CategoryValidationMessages.NameNullCode)
            .MinimumLength(CategoryValidationMessages.NameMinLength)
            .WithMessage(CategoryValidationMessages.NameTooShort)
            .WithErrorCode(CategoryValidationMessages.NameTooShortCode)
            .MaximumLength(CategoryValidationMessages.NameMaxLength)
            .WithMessage(CategoryValidationMessages.NameTooLong)
            .WithErrorCode(CategoryValidationMessages.NameTooLongCode)
            .Matches(CategoryValidationMessages.NameRegexPattern)
            .WithMessage(CategoryValidationMessages.NameInvalidFormat)
            .WithErrorCode(CategoryValidationMessages.NameInvalidFormatCode)
            .Must(NotContainMultipleSpaces)
            .WithMessage(CategoryValidationMessages.NameMultipleSpaces)
            .WithErrorCode(CategoryValidationMessages.NameMultipleSpacesCode)
            .Must(NotStartOrEndWithSpace)
            .WithMessage(CategoryValidationMessages.NameSpaceEdges)
            .WithErrorCode(CategoryValidationMessages.NameSpaceEdgesCode);

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

    private bool NotContainMultipleSpaces(string name)
    {
        return !name.Contains("  ");
    }

    private bool NotStartOrEndWithSpace(string name)
    {
        return name == name.Trim();
    }
}