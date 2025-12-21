using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CategoryFeature.Rules;
using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;

public class CreateCategoryCommandHandler(
    IWriteCategoryRepository writeCategoryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICategoryBusinessRules categoryBusinessRules
    ) : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
{
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {

        var businessRulesResult = await  BusinessRuleEngine.RunAsync(
            () => categoryBusinessRules.CheckCategoryNameIsUnique(request.CreateCategoryCommandRequestDto!.Name!),
            () => categoryBusinessRules.CheckMaxCategoryLimitNotReached()
        );

        if (businessRulesResult.IsFail)
        {
            return new CreateCategoryCommandResponse
            {
                Result = businessRulesResult
            };
        }

        var addedCategory = mapper.Map<Category>(request.CreateCategoryCommandRequestDto);

        await writeCategoryRepository.AddAsync(addedCategory);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResponse
        {
            Result = ServiceResult.SuccessAsOk("Category created successfully.")
        };
    }
}