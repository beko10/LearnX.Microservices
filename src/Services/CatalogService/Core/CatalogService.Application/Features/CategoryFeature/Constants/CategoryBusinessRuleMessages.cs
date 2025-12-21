namespace CatalogService.Application.Constants.BusinessRuleMessages;

public static class CategoryBusinessRuleMessages
{
    // Existence Messages
    public static string CategoryNotFound(string categoryId)
        => $"Category with id '{categoryId}' not found";

    public const string CategoryNotFoundGeneric = "Category not found";

    // Uniqueness Messages
    public static string CategoryNameAlreadyExists(string name)
        => $"Category with name '{name}' already exists";

    // System Constraints
    public static string MaxCategoryLimitReached(int limit)
        => $"Maximum category limit ({limit}) has been reached";

    public const int MaxCategoriesLimit = 50;

    // Delete Validation Messages
    public static string CannotDeleteCategoryWithCourses(int courseCount)
        => $"Cannot delete category. It has {courseCount} course(s). Remove all courses first.";

    public const string CategoryHasCoursesCannotDelete = "Cannot delete category with courses";

    // State Messages
    public const string CategoryNotActive = "Category is not active";
    public const string CategoryAlreadyActive = "Category is already active";
    public const string CategoryAlreadyInactive = "Category is already inactive";

    public static string CannotDeactivateCategoryWithCourses(int courseCount)
        => $"Cannot deactivate category with {courseCount} active course(s)";
}