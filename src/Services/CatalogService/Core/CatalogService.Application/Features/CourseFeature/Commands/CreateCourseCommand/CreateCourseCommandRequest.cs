using CatalogService.Application.Features.CourseFeature.DTOs;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.CreateCourseCommand;

public class CreateCourseCommandRequest : IRequest<CreateCourseCommandResponse>
{
    public CreateCourseCommandDto? CreateCourseCommandRequestDto { get; set; }
}

