using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.CreateCourseCommand;

public class CreateCourseCommandHandler(
    IWriteCourseRepository writeCourseRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICourseBusinessRules courseBusinessRules
    ) : IRequestHandler<CreateCourseCommandRequest, CreateCourseCommandResponse>
{
    public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommandRequest request, CancellationToken cancellationToken)
    {

        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => courseBusinessRules.CheckCourseTitleIsUnique(request.CreateCourseCommandRequestDto!.Title!),
            () => courseBusinessRules.CheckMaxCourseLimitNotReached(),
            () => courseBusinessRules.CheckCategoryExists(request.CreateCourseCommandRequestDto!.CategoryId),
            () => Task.FromResult(courseBusinessRules.CheckPriceIsValid(request.CreateCourseCommandRequestDto!.Price))
        );

        if (businessRulesResult.IsFail)
        {
            return new CreateCourseCommandResponse
            {
                Result = businessRulesResult
            };
        }

        var addedCourse = mapper.Map<Course>(request.CreateCourseCommandRequestDto);

        await writeCourseRepository.AddAsync(addedCourse);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCourseCommandResponse
        {
            Result = ServiceResult.SuccessAsOk("Course created successfully.")
        };
    }
}

