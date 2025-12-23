using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Queries.GetByIdCourseQuery;

public class GetByIdCourseQueryRequest : IRequest<GetByIdCourseQueryResponse>
{
    public string Id { get; set; }
}

