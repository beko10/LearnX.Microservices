using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.DTOs;

namespace CatalogService.Application.Features.CourseFeature.Queries.GetByIdCourseQuery;

public class GetByIdCourseQueryResponse
{
    public ServiceResult<GetByIdCourseQueryDto> Result { get; set; } = null!;
}

