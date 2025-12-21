// CatalogService.Application/Features/Categories/CreateCategory/CreateCategoryCommandValidator.cs
using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CategoryFeature.DTOs;
using FluentValidation;

namespace CatalogService.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandDtoValidator : AbstractValidator<CreateCategoryCommandDto>
{
    public CreateCategoryCommandDtoValidator()
    {
        RuleFor(x => x.Name)
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