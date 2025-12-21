namespace CatalogService.Application.Constants.BusinessRuleMessages;

public static class CourseBusinessRuleMessages
{
    // Existence Messages
    public static string CourseNotFound(string courseId)
        => $"Course with id '{courseId}' not found";

    public const string CourseNotFoundGeneric = "Course not found";

    // Uniqueness Messages
    public static string CourseTitleAlreadyExists(string title)
        => $"Course with title '{title}' already exists";

    // System Constraints
    public static string MaxCourseLimitReached(int limit)
        => $"Maximum course limit ({limit}) has been reached";

    public const int MaxCoursesLimit = 1000;

    // Category Validation Messages
    public static string CategoryNotFound(string categoryId)
        => $"Category with id '{categoryId}' not found";

    public const string CategoryNotFoundGeneric = "Category not found for course";

    // Price Validation Messages
    public const string PriceMustBePositive = "Course price must be positive";
    public const string PriceTooHigh = "Course price exceeds maximum allowed value";

    public const decimal MaxPrice = 99999.99m;
    public const decimal MinPrice = 0.01m;
}

