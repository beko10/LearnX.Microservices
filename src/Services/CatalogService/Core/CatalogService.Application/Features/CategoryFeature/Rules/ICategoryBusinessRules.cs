using BuildingBlocks.Core.Results;

namespace CatalogService.Application.Features.CategoryFeature.Rules;

public interface ICategoryBusinessRules
{
    // ============== EXISTENCE & UNIQUENESS ==============
    Task<ServiceResult> CheckCategoryExists(string categoryId);
    Task<ServiceResult> CheckCategoryNameIsUnique(string name);
    Task<ServiceResult> CheckCategoryNameIsUniqueExceptCurrent(string name, string currentId);

    // ============== SYSTEM CONSTRAINTS ==============
    Task<ServiceResult> CheckMaxCategoryLimitNotReached();


    // ============== DELETE VALIDATION ==============
    Task<ServiceResult> CheckCategoryCanBeDeleted(string categoryId);
    Task<ServiceResult> CheckCategoryHasNoCourses(string categoryId);
}
