namespace CatalogService.Application.Constants.ValidationMessages;

public static class CategoryValidationMessages
{
    // Name Validation Messages
    public const string NameRequired = "Category name is required";
    public const string NameRequiredCode = "CATEGORY_NAME_REQUIRED";

    public const string NameNull = "Category name cannot be null";
    public const string NameNullCode = "CATEGORY_NAME_NULL";

    public const string NameTooShort = "Category name must be at least {MinLength} characters long";
    public const string NameTooShortCode = "CATEGORY_NAME_TOO_SHORT";

    public const string NameTooLong = "Category name cannot exceed {MaxLength} characters";
    public const string NameTooLongCode = "CATEGORY_NAME_TOO_LONG";

    public const string NameInvalidFormat = "Category name can only contain letters, numbers and spaces";
    public const string NameInvalidFormatCode = "CATEGORY_NAME_INVALID_FORMAT";

    public const string NameMultipleSpaces = "Category name cannot contain multiple consecutive spaces";
    public const string NameMultipleSpacesCode = "CATEGORY_NAME_MULTIPLE_SPACES";

    public const string NameSpaceEdges = "Category name cannot start or end with a space";
    public const string NameSpaceEdgesCode = "CATEGORY_NAME_SPACE_EDGES";

    // Id Validation Messages
    public const string IdRequired = "Category id is required";
    public const string IdRequiredCode = "CATEGORY_ID_REQUIRED";

    public const string IdNull = "Category id cannot be null";
    public const string IdNullCode = "CATEGORY_ID_NULL";

    public const string IdInvalid = "Category id is invalid";
    public const string IdInvalidCode = "CATEGORY_ID_INVALID";

    // Validation Constraints
    public const int NameMinLength = 2;
    public const int NameMaxLength = 100;
    public const string NameRegexPattern = @"^[a-zA-Z0-9\sığüşöçİĞÜŞÖÇ]+$";
}