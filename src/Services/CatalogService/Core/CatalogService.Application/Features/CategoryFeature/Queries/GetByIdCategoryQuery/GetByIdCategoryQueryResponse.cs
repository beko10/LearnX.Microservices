using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CategoryFeature.DTOs;

namespace CatalogService.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

public class GetByIdCategoryQueryResponse
{
    public ServiceResult<GetByIdCategoryQueryDto> Result { get; set; } = null!;
}
