using BuildingBlocks.Core.Results;
using CatalogService.Application.Constants.BusinessRuleMessages;
using CatalogService.Application.Features.CategoryFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;

namespace CatalogService.Application.BusinessRules;

public class CategoryBusinessRules(
    IReadCategoryRepository categoryReadRepository,
    IReadCourseRepository courseReadRepository
    ) : ICategoryBusinessRules
{


    // ============== EXISTENCE & UNIQUENESS ==============

    public async Task<ServiceResult> CheckCategoryExists(string categoryId)
    {
        var exists = await categoryReadRepository.ExistsAsync(
            c => c.Id == categoryId);

        return exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.ErrorAsNotFound(
                CategoryBusinessRuleMessages.CategoryNotFound(categoryId));
    }

    public async Task<ServiceResult> CheckCategoryNameIsUnique(string name)
    {
        // MongoDB case-insensitive sorgu için tüm kategorileri alıp memory'de kontrol ediyoruz
        var categories = await categoryReadRepository.GetAllAsync();
        var exists = categories.Any(c =>
            string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

        return !exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CategoryBusinessRuleMessages.CategoryNameAlreadyExists(name));
    }

    public async Task<ServiceResult> CheckCategoryNameIsUniqueExceptCurrent(string name, string currentId)
    {
        // MongoDB case-insensitive sorgu için tüm kategorileri alıp memory'de kontrol ediyoruz
        var categories = await categoryReadRepository.GetAllAsync();
        var category = categories.FirstOrDefault(c =>
            string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

        if (category == null)
            return ServiceResult.SuccessAsNoContent();

        if (category.Id == currentId)
            return ServiceResult.SuccessAsNoContent();

        return ServiceResult.Fail(
            CategoryBusinessRuleMessages.CategoryNameAlreadyExists(name));
    }

    // ============== SYSTEM CONSTRAINTS ==============

    public async Task<ServiceResult> CheckMaxCategoryLimitNotReached()
    {
        var count = await categoryReadRepository.CountAsync();

        return count < CategoryBusinessRuleMessages.MaxCategoriesLimit
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CategoryBusinessRuleMessages.MaxCategoryLimitReached(
                    CategoryBusinessRuleMessages.MaxCategoriesLimit));
    }

    // ============== DELETE VALIDATION ==============

    public async Task<ServiceResult> CheckCategoryCanBeDeleted(string categoryId)
    {
        var existsResult = await CheckCategoryExists(categoryId);
        if (!existsResult.IsSuccess)
            return existsResult;

        var hasCoursesResult = await CheckCategoryHasNoCourses(categoryId);
        if (!hasCoursesResult.IsSuccess)
            return hasCoursesResult;

        return ServiceResult.SuccessAsNoContent();
    }

    public async Task<ServiceResult> CheckCategoryHasNoCourses(string categoryId)
    {
        var courseCount = await courseReadRepository.CountWhereAsync(
            c => c.CategoryId == categoryId);

        return courseCount == 0
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CategoryBusinessRuleMessages.CannotDeleteCategoryWithCourses(courseCount));
    }
}