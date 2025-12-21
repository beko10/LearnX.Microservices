using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.UpdateCourseCommand;

public class UpdateCourseCommandHandler(
    IWriteCourseRepository writeCourseRepository,
    ICourseBusinessRules courseBusinessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<UpdateCourseCommandRequest, UpdateCourseCommandResponse>
{
    public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => courseBusinessRules.CheckCourseExists(request.UpdateCourseCommandRequestDto!.Id.ToString()),
            () => courseBusinessRules.CheckCourseTitleIsUniqueExceptCurrent(request.UpdateCourseCommandRequestDto!.Title, request.UpdateCourseCommandRequestDto!.Id.ToString()),
            () => courseBusinessRules.CheckCategoryExists(request.UpdateCourseCommandRequestDto!.CategoryId.ToString()),
            () => Task.FromResult(courseBusinessRules.CheckPriceIsValid(request.UpdateCourseCommandRequestDto!.Price))
        );

        if (businessRulesResult.IsFail)
        {
            return new UpdateCourseCommandResponse
            {
                Result = businessRulesResult
            };
        }

        var updatedCourse = mapper.Map<Course>(request.UpdateCourseCommandRequestDto);

        await writeCourseRepository.UpdateAsync(updatedCourse);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateCourseCommandResponse
        {
            Result = ServiceResult.SuccessAsOk("Course updated successfully.")
        };
    }
}

