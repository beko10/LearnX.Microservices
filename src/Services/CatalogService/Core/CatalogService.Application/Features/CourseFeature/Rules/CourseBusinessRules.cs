using BuildingBlocks.Core.Results;
using CatalogService.Application.Constants.BusinessRuleMessages;
using CatalogService.Application.Features.CourseFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;

namespace CatalogService.Application.BusinessRules;

public class CourseBusinessRules(
    IReadCourseRepository courseReadRepository,
    IReadCategoryRepository categoryReadRepository
    ) : ICourseBusinessRules
{

    // ============== EXISTENCE & UNIQUENESS ==============

    public async Task<ServiceResult> CheckCourseExists(string courseId)
    {
        var exists = await courseReadRepository.ExistsAsync(
            c => c.Id == courseId);

        return exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.ErrorAsNotFound(
                CourseBusinessRuleMessages.CourseNotFound(courseId));
    }

    public async Task<ServiceResult> CheckCourseTitleIsUnique(string title)
    {
        var exists = await courseReadRepository.ExistsAsync(
            c => c.Title.ToLower() == title.ToLower());

        return !exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CourseBusinessRuleMessages.CourseTitleAlreadyExists(title));
    }

    public async Task<ServiceResult> CheckCourseTitleIsUniqueExceptCurrent(string title, string currentId)
    {
        var course = await courseReadRepository.GetSingleAsync(
            c => c.Title.ToLower() == title.ToLower());

        if (course == null)
            return ServiceResult.SuccessAsNoContent();

        if (course.Id == currentId)
            return ServiceResult.SuccessAsNoContent();

        return ServiceResult.Fail(
            CourseBusinessRuleMessages.CourseTitleAlreadyExists(title));
    }

    // ============== SYSTEM CONSTRAINTS ==============

    public async Task<ServiceResult> CheckMaxCourseLimitNotReached()
    {
        var count = await courseReadRepository.CountAsync();

        return count < CourseBusinessRuleMessages.MaxCoursesLimit
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CourseBusinessRuleMessages.MaxCourseLimitReached(
                    CourseBusinessRuleMessages.MaxCoursesLimit));
    }

    // ============== CATEGORY VALIDATION ==============

    public async Task<ServiceResult> CheckCategoryExists(string categoryId)
    {
        var exists = await categoryReadRepository.ExistsAsync(
            c => c.Id == categoryId);

        return exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail(
                CourseBusinessRuleMessages.CategoryNotFound(categoryId));
    }

    // ============== PRICE VALIDATION ==============

    public ServiceResult CheckPriceIsValid(decimal price)
    {
        if (price < CourseBusinessRuleMessages.MinPrice)
            return ServiceResult.Fail(CourseBusinessRuleMessages.PriceMustBePositive);

        if (price > CourseBusinessRuleMessages.MaxPrice)
            return ServiceResult.Fail(CourseBusinessRuleMessages.PriceTooHigh);

        return ServiceResult.SuccessAsNoContent();
    }
}

