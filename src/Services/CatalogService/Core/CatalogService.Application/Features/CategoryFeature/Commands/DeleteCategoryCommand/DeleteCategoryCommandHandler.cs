

using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CategoryFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;

public class DeleteCategoryCommandHandler(
    IWriteCategoryRepository writeCategoryRepository,
    ICategoryBusinessRules categoryBusinessRules,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
{
    public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await  BusinessRuleEngine.RunAsync(
            () => categoryBusinessRules.CheckCategoryExists(request.Id),
            () => categoryBusinessRules.CheckCategoryCanBeDeleted(request.Id)
        );

        if(businessRulesResult.IsFail)
        {
            return new DeleteCategoryCommandResponse
            {
                Result = businessRulesResult
            };
        }
        
        await writeCategoryRepository.RemoveIdAsync(request.Id.ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new DeleteCategoryCommandResponse
        {
            Result = ServiceResult.SuccessAsOk("Category deleted successfully.")
        };
    }
}
