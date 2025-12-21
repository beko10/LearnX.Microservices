using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.DTOs;

namespace CatalogService.Application.Features.CourseFeature.Queries.GetAllCourseQuery;

public class GetAllCourseQueryResponse
{
    public ServiceResult<IEnumerable<GetAllCourseQueryDto>> Result { get; set; } = null!;
}

