using AutoMapper;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.DTOs;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Queries.GetByIdCourseQuery;

public class GetByIdCourseQueryHandler(
    IReadCourseRepository readCourseRepository,
    IMapper mapper
    ) : IRequestHandler<GetByIdCourseQueryRequest, GetByIdCourseQueryResponse>
{
    public async Task<GetByIdCourseQueryResponse> Handle(GetByIdCourseQueryRequest request, CancellationToken cancellationToken)
    {
        var course = await readCourseRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);

        if (course is null)
        {
            return new GetByIdCourseQueryResponse
            {
                Result = ServiceResult<GetByIdCourseQueryDto>.ErrorAsNotFound()
            };
        }
        var mappedCourse = mapper.Map<GetByIdCourseQueryDto>(course);
        return new GetByIdCourseQueryResponse
        {
            Result = ServiceResult<GetByIdCourseQueryDto>.SuccessAsOk(mappedCourse)
        };
    }
}

