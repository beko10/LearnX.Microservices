using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Queries.GetByIdCourseQuery;

public class GetByIdCourseQueryRequest : IRequest<GetByIdCourseQueryResponse>
{
    public Guid Id { get; set; }
}

