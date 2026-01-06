using AutoMapper;
using BuildingBlocks.Core.Results;
using CatalogService.Application.Features.CourseFeature.DTOs;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Queries.GetAllCourseQuery;

public class GetAllCourseQueryHandler(
    IReadCourseRepository readCourseRepository,
    IMapper mapper
    ) : IRequestHandler<GetAllCourseQueryRequest, GetAllCourseQueryResponse>
{
    public async Task<GetAllCourseQueryResponse> Handle(GetAllCourseQueryRequest request, CancellationToken cancellationToken)
    {
        var courses = await readCourseRepository.GetAllAsync(cancellationToken);
        var mappedCourses = mapper.Map<IEnumerable<GetAllCourseQueryDto>>(courses);
        return new GetAllCourseQueryResponse
        {
            Result = ServiceResult<IEnumerable<GetAllCourseQueryDto>>.SuccessAsOk(mappedCourses)
        };
    }
}

