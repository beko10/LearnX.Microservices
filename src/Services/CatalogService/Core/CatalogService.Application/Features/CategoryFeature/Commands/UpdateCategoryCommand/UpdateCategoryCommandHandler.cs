using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;
using CatalogService.Application.Features.CategoryFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;

public class UpdateCategoryCommandHandler(
    IReadCategoryRepository readCategoryRepository, // 👈 EKLE
    IWriteCategoryRepository writeCategoryRepository,
    ICategoryBusinessRules categoryBusinessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
{
    public async Task<UpdateCategoryCommandResponse> Handle(
        UpdateCategoryCommandRequest request,
        CancellationToken cancellationToken)
    {
        
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => categoryBusinessRules.CheckCategoryExists(request.UpdateCategoryCommandRequestDto!.Id),
            () => categoryBusinessRules.CheckCategoryNameIsUniqueExceptCurrent(
                request.UpdateCategoryCommandRequestDto!.Name,
                request.UpdateCategoryCommandRequestDto!.Id)
        );

        if (businessRulesResult.IsFail)
        {
            return new UpdateCategoryCommandResponse
            {
                Result = businessRulesResult
            };
        }

       
        var existingCategory = await readCategoryRepository.GetByIdAsync(
            request.UpdateCategoryCommandRequestDto!.Id,
            cancellationToken);

        
        mapper.Map(request.UpdateCategoryCommandRequestDto, existingCategory);

        
        await writeCategoryRepository.UpdateAsync(existingCategory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryCommandResponse
        {
            Result = ServiceResult.SuccessAsOk("Category updated successfully.")
        };
    }
}