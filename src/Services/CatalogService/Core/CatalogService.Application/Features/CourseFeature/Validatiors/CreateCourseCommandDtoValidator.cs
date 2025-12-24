using CatalogService.Application.Constants.ValidationMessages;
using CatalogService.Application.Features.CourseFeature.DTOs;
using FluentValidation;
using MongoDB.Bson;

namespace CatalogService.Application.Features.CourseFeature.Validatiors;

public class CreateCourseCommandDtoValidator : AbstractValidator<CreateCourseCommandDto>
{
    public CreateCourseCommandDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.TitleRequired)
            .WithErrorCode(CourseValidationMessages.TitleRequiredCode)
            .NotNull()
            .WithMessage(CourseValidationMessages.TitleNull)
            .WithErrorCode(CourseValidationMessages.TitleNullCode)
            .MinimumLength(CourseValidationMessages.TitleMinLength)
            .WithMessage(CourseValidationMessages.TitleTooShort)
            .WithErrorCode(CourseValidationMessages.TitleTooShortCode)
            .MaximumLength(CourseValidationMessages.TitleMaxLength)
            .WithMessage(CourseValidationMessages.TitleTooLong)
            .WithErrorCode(CourseValidationMessages.TitleTooLongCode)
            .Matches(CourseValidationMessages.TitleRegexPattern)
            .WithMessage(CourseValidationMessages.TitleInvalidFormat)
            .WithErrorCode(CourseValidationMessages.TitleInvalidFormatCode)
            .Must(NotContainMultipleSpaces)
            .WithMessage(CourseValidationMessages.TitleMultipleSpaces)
            .WithErrorCode(CourseValidationMessages.TitleMultipleSpacesCode)
            .Must(NotStartOrEndWithSpace)
            .WithMessage(CourseValidationMessages.TitleSpaceEdges)
            .WithErrorCode(CourseValidationMessages.TitleSpaceEdgesCode);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.DescriptionRequired)
            .WithErrorCode(CourseValidationMessages.DescriptionRequiredCode)
            .NotNull()
            .WithMessage(CourseValidationMessages.DescriptionNull)
            .WithErrorCode(CourseValidationMessages.DescriptionNullCode)
            .MinimumLength(CourseValidationMessages.DescriptionMinLength)
            .WithMessage(CourseValidationMessages.DescriptionTooShort)
            .WithErrorCode(CourseValidationMessages.DescriptionTooShortCode)
            .MaximumLength(CourseValidationMessages.DescriptionMaxLength)
            .WithMessage(CourseValidationMessages.DescriptionTooLong)
            .WithErrorCode(CourseValidationMessages.DescriptionTooLongCode);

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.PriceRequired)
            .WithErrorCode(CourseValidationMessages.PriceRequiredCode)
            .GreaterThanOrEqualTo(CourseValidationMessages.PriceMinValue)
            .WithMessage(CourseValidationMessages.PriceTooLow)
            .WithErrorCode(CourseValidationMessages.PriceTooLowCode)
            .LessThanOrEqualTo(CourseValidationMessages.PriceMaxValue)
            .WithMessage(CourseValidationMessages.PriceTooHigh)
            .WithErrorCode(CourseValidationMessages.PriceTooHighCode);

        RuleFor(x => x.Picture)
            .MaximumLength(CourseValidationMessages.PictureMaxLength)
            .WithMessage(CourseValidationMessages.PictureTooLong)
            .WithErrorCode(CourseValidationMessages.PictureTooLongCode)
            .When(x => !string.IsNullOrEmpty(x.Picture));

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.UserIdRequired)
            .WithErrorCode(CourseValidationMessages.UserIdRequiredCode)
            .NotNull()
            .WithMessage(CourseValidationMessages.UserIdNull)
            .WithErrorCode(CourseValidationMessages.UserIdNullCode)
            .Must(BeAValidGuid)
            .WithMessage(CourseValidationMessages.UserIdInvalid)
            .WithErrorCode(CourseValidationMessages.UserIdInvalidCode);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.CategoryIdRequired)
            .WithErrorCode(CourseValidationMessages.CategoryIdRequiredCode)
            .NotNull()
            .WithMessage(CourseValidationMessages.CategoryIdNull)
            .WithErrorCode(CourseValidationMessages.CategoryIdNullCode)
            .Must(BeAValidObjectId)
            .WithMessage(CourseValidationMessages.CategoryIdInvalid)
            .WithErrorCode(CourseValidationMessages.CategoryIdInvalidCode);

        RuleFor(x => x.Feature)
            .NotNull()
            .WithMessage(CourseValidationMessages.FeatureRequired)
            .WithErrorCode(CourseValidationMessages.FeatureRequiredCode)
            .SetValidator(new CreateCourseFeatureDtoValidator());
    }

    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out var guid) && guid != Guid.Empty;
    }

    private bool BeAValidObjectId(string id)
    {
        return MongoDB.Bson.ObjectId.TryParse(id, out _);
    }

    private bool NotContainMultipleSpaces(string title)
    {
        return !title.Contains("  ");
    }

    private bool NotStartOrEndWithSpace(string title)
    {
        return title == title.Trim();
    }
}

public class CreateCourseFeatureDtoValidator : AbstractValidator<CreateCourseFeatureDto>
{
    public CreateCourseFeatureDtoValidator()
    {
        RuleFor(x => x.Duration)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.FeatureDurationRequired)
            .WithErrorCode(CourseValidationMessages.FeatureDurationRequiredCode)
            .NotNull()
            .WithMessage(CourseValidationMessages.FeatureDurationRequired)
            .WithErrorCode(CourseValidationMessages.FeatureDurationRequiredCode);

        RuleFor(x => x.Rating)
            .InclusiveBetween(CourseValidationMessages.RatingMinValue, CourseValidationMessages.RatingMaxValue)
            .WithMessage(CourseValidationMessages.FeatureRatingInvalid)
            .WithErrorCode(CourseValidationMessages.FeatureRatingInvalidCode);

        RuleFor(x => x.EducatorFullName)
            .NotEmpty()
            .WithMessage(CourseValidationMessages.FeatureEducatorFullNameRequired)
            .WithErrorCode(CourseValidationMessages.FeatureEducatorFullNameRequiredCode)
            .NotNull()
            .WithMessage(CourseValidationMessages.FeatureEducatorFullNameRequired)
            .WithErrorCode(CourseValidationMessages.FeatureEducatorFullNameRequiredCode)
            .MaximumLength(CourseValidationMessages.EducatorFullNameMaxLength)
            .WithMessage($"Educator full name cannot exceed {CourseValidationMessages.EducatorFullNameMaxLength} characters");
    }
}

