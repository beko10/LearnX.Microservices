using BuildingBlocks.Core.Results;

namespace CatalogService.Application.Features.CourseFeature.Rules;

public interface ICourseBusinessRules
{
    // ============== EXISTENCE & UNIQUENESS ==============
    Task<ServiceResult> CheckCourseExists(string courseId);
    Task<ServiceResult> CheckCourseTitleIsUnique(string title);
    Task<ServiceResult> CheckCourseTitleIsUniqueExceptCurrent(string title, string currentId);

    // ============== SYSTEM CONSTRAINTS ==============
    Task<ServiceResult> CheckMaxCourseLimitNotReached();

    // ============== CATEGORY VALIDATION ==============
    Task<ServiceResult> CheckCategoryExists(string categoryId);

    // ============== PRICE VALIDATION ==============
    ServiceResult CheckPriceIsValid(decimal price);
}

