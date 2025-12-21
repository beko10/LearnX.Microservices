using AutoMapper;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CategoryFeature.DTOs;
using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Queries.GetAllCategoryQuery;

public class GetAllCategoryQueryHandler(
    IReadCategoryRepository readCategoryRepository,
    IMapper mapper
    ) : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryResponse>
{
    public async Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var categories = await readCategoryRepository.GetAllAsync(cancellationToken);
        if (categories.Count() == 0)
        {
            return new GetAllCategoryQueryResponse{
                Result = ServiceResult<IEnumerable<GetAllCategoryQueryDto>>.ErrorAsNotFound()
            };
        }
        var mappedCategories = mapper.Map<IEnumerable<GetAllCategoryQueryDto>>(categories);
        return new GetAllCategoryQueryResponse{
            Result = ServiceResult<IEnumerable<GetAllCategoryQueryDto>>.SuccessAsOk(mappedCategories)
        };
    }
}
