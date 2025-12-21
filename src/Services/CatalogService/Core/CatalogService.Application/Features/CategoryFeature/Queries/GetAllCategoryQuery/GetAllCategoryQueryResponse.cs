using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CategoryFeature.DTOs;

namespace CatalogService.Application.Features.CategoryFeature.Queries.GetAllCategoryQuery;

public class GetAllCategoryQueryResponse
{
    public ServiceResult<IEnumerable<GetAllCategoryQueryDto>> Result { get; set; } = null!;
}
