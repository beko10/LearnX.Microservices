namespace CatalogService.Application.Constants.ValidationMessages;

public static class CourseValidationMessages
{
    // Title Validation Messages
    public const string TitleRequired = "Course title is required";
    public const string TitleRequiredCode = "COURSE_TITLE_REQUIRED";

    public const string TitleNull = "Course title cannot be null";
    public const string TitleNullCode = "COURSE_TITLE_NULL";

    public const string TitleTooShort = "Course title must be at least {MinLength} characters long";
    public const string TitleTooShortCode = "COURSE_TITLE_TOO_SHORT";

    public const string TitleTooLong = "Course title cannot exceed {MaxLength} characters";
    public const string TitleTooLongCode = "COURSE_TITLE_TOO_LONG";

    public const string TitleInvalidFormat = "Course title can only contain letters, numbers and spaces";
    public const string TitleInvalidFormatCode = "COURSE_TITLE_INVALID_FORMAT";

    public const string TitleMultipleSpaces = "Course title cannot contain multiple consecutive spaces";
    public const string TitleMultipleSpacesCode = "COURSE_TITLE_MULTIPLE_SPACES";

    public const string TitleSpaceEdges = "Course title cannot start or end with a space";
    public const string TitleSpaceEdgesCode = "COURSE_TITLE_SPACE_EDGES";

    // Description Validation Messages
    public const string DescriptionRequired = "Course description is required";
    public const string DescriptionRequiredCode = "COURSE_DESCRIPTION_REQUIRED";

    public const string DescriptionNull = "Course description cannot be null";
    public const string DescriptionNullCode = "COURSE_DESCRIPTION_NULL";

    public const string DescriptionTooShort = "Course description must be at least {MinLength} characters long";
    public const string DescriptionTooShortCode = "COURSE_DESCRIPTION_TOO_SHORT";

    public const string DescriptionTooLong = "Course description cannot exceed {MaxLength} characters";
    public const string DescriptionTooLongCode = "COURSE_DESCRIPTION_TOO_LONG";

    // Price Validation Messages
    public const string PriceRequired = "Course price is required";
    public const string PriceRequiredCode = "COURSE_PRICE_REQUIRED";

    public const string PriceInvalid = "Course price must be a valid positive number";
    public const string PriceInvalidCode = "COURSE_PRICE_INVALID";

    public const string PriceTooLow = "Course price must be at least {MinPrice}";
    public const string PriceTooLowCode = "COURSE_PRICE_TOO_LOW";

    public const string PriceTooHigh = "Course price cannot exceed {MaxPrice}";
    public const string PriceTooHighCode = "COURSE_PRICE_TOO_HIGH";

    // Picture Validation Messages
    public const string PictureInvalidFormat = "Picture URL format is invalid";
    public const string PictureInvalidFormatCode = "COURSE_PICTURE_INVALID_FORMAT";

    public const string PictureTooLong = "Picture URL cannot exceed {MaxLength} characters";
    public const string PictureTooLongCode = "COURSE_PICTURE_TOO_LONG";

    // UserId Validation Messages
    public const string UserIdRequired = "User id is required";
    public const string UserIdRequiredCode = "COURSE_USER_ID_REQUIRED";

    public const string UserIdNull = "User id cannot be null";
    public const string UserIdNullCode = "COURSE_USER_ID_NULL";

    public const string UserIdInvalid = "User id is invalid";
    public const string UserIdInvalidCode = "COURSE_USER_ID_INVALID";

    // CategoryId Validation Messages
    public const string CategoryIdRequired = "Category id is required";
    public const string CategoryIdRequiredCode = "COURSE_CATEGORY_ID_REQUIRED";

    public const string CategoryIdNull = "Category id cannot be null";
    public const string CategoryIdNullCode = "COURSE_CATEGORY_ID_NULL";

    public const string CategoryIdInvalid = "Category id is invalid";
    public const string CategoryIdInvalidCode = "COURSE_CATEGORY_ID_INVALID";

    // Feature Validation Messages
    public const string FeatureRequired = "Course feature is required";
    public const string FeatureRequiredCode = "COURSE_FEATURE_REQUIRED";

    public const string FeatureDurationRequired = "Feature duration is required";
    public const string FeatureDurationRequiredCode = "COURSE_FEATURE_DURATION_REQUIRED";

    public const string FeatureRatingInvalid = "Feature rating must be between 0 and 5";
    public const string FeatureRatingInvalidCode = "COURSE_FEATURE_RATING_INVALID";

    public const string FeatureEducatorFullNameRequired = "Feature educator full name is required";
    public const string FeatureEducatorFullNameRequiredCode = "COURSE_FEATURE_EDUCATOR_FULL_NAME_REQUIRED";

    // Id Validation Messages
    public const string IdRequired = "Course id is required";
    public const string IdRequiredCode = "COURSE_ID_REQUIRED";

    public const string IdNull = "Course id cannot be null";
    public const string IdNullCode = "COURSE_ID_NULL";

    public const string IdInvalid = "Course id is invalid";
    public const string IdInvalidCode = "COURSE_ID_INVALID";

    // Validation Constraints
    public const int TitleMinLength = 3;
    public const int TitleMaxLength = 200;
    public const string TitleRegexPattern = @"^[a-zA-Z0-9\sığüşöçİĞÜŞÖÇ]+$";

    public const int DescriptionMinLength = 10;
    public const int DescriptionMaxLength = 5000;

    public const decimal PriceMinValue = 0.01m;
    public const decimal PriceMaxValue = 99999.99m;

    public const int PictureMaxLength = 500;

    public const float RatingMinValue = 0.0f;
    public const float RatingMaxValue = 5.0f;

    public const int EducatorFullNameMaxLength = 100;
}

