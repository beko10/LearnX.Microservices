using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

public class GetByIdCategoryQueryRequest : IRequest<GetByIdCategoryQueryResponse>
{
    public Guid Id { get; set; }
}
