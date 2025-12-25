using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.UpdateCourseCommand;

public class UpdateCourseCommandHandler(
    IWriteCourseRepository writeCourseRepository,
    IReadCourseRepository readCourseRepository,
    ICourseBusinessRules courseBusinessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<UpdateCourseCommandRequest, UpdateCourseCommandResponse>
{
    public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => courseBusinessRules.CheckCourseExists(request.Id),
            () => courseBusinessRules.CheckCourseTitleIsUniqueExceptCurrent(request.UpdateCourseCommandRequestDto!.Title, request.Id),
            () => courseBusinessRules.CheckCategoryExists(request.UpdateCourseCommandRequestDto!.CategoryId),
            () => Task.FromResult(courseBusinessRules.CheckPriceIsValid(request.UpdateCourseCommandRequestDto!.Price))
        );

        if (businessRulesResult.IsFail)
        {
            return new UpdateCourseCommandResponse
            {
                Result = businessRulesResult
            };
        }

        var existingCourse = await readCourseRepository.GetByIdAsync(request.Id, cancellationToken);

        mapper.Map(request.UpdateCourseCommandRequestDto, existingCourse);

        await writeCourseRepository.UpdateAsync(existingCourse,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateCourseCommandResponse
        {
            Result = ServiceResult.SuccessAsOk("Course updated successfully.")
        };
    }
}

