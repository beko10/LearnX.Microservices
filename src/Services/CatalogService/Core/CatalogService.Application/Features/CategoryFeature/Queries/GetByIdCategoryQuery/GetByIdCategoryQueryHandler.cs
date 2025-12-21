using AutoMapper;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CategoryFeature.DTOs;
using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

public class GetByIdCategoryQueryHandler(
    IReadCategoryRepository readCategoryRepository,
    IMapper mapper
    ) : IRequestHandler<GetByIdCategoryQueryRequest, GetByIdCategoryQueryResponse>
{
    public async Task<GetByIdCategoryQueryResponse> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var category = await readCategoryRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);

        if (category is null)
        {
            return new GetByIdCategoryQueryResponse
            {
                Result = ServiceResult<GetByIdCategoryQueryDto>.ErrorAsNotFound()
            };
        }
        var mappedCategory = mapper.Map<GetByIdCategoryQueryDto>(category);
        return new GetByIdCategoryQueryResponse
        {
            Result = ServiceResult<GetByIdCategoryQueryDto>.SuccessAsOk(mappedCategory)
        };
    }
}
