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
        var exists = await categoryReadRepository.ExistsAsync(
            c => c.Name.ToLower() == name.ToLower());

        return !exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CategoryBusinessRuleMessages.CategoryNameAlreadyExists(name));
    }

    public async Task<ServiceResult> CheckCategoryNameIsUniqueExceptCurrent(string name, string currentId)
    {
        var category = await categoryReadRepository.GetSingleAsync(
            c => c.Name.ToLower() == name.ToLower());

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
            c => c.CategoryId.ToString() == categoryId);

        return courseCount == 0
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CategoryBusinessRuleMessages.CannotDeleteCategoryWithCourses(courseCount));
    }
}