using CatalogService.Application.Features.CourseFeature.DTOs;
using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.UpdateCourseCommand;

public class UpdateCourseCommandRequest : IRequest<UpdateCourseCommandResponse>
{
    public UpdateCourseCommandDto? UpdateCourseCommandRequestDto { get; set; }
}

