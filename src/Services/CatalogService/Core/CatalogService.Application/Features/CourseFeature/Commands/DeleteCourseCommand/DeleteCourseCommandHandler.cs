using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.DeleteCourseCommand;

public class DeleteCourseCommandHandler(
    IWriteCourseRepository writeCourseRepository,
    ICourseBusinessRules courseBusinessRules,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCourseCommandRequest, DeleteCourseCommandResponse>
{
    public async Task<DeleteCourseCommandResponse> Handle(DeleteCourseCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => courseBusinessRules.CheckCourseExists(request.Id.ToString())
        );

        if (businessRulesResult.IsFail)
        {
            return new DeleteCourseCommandResponse
            {
                Result = businessRulesResult
            };
        }

        await writeCourseRepository.RemoveIdAsync(request.Id.ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new DeleteCourseCommandResponse
        {
            Result = ServiceResult.SuccessAsOk("Course deleted successfully.")
        };
    }
}

