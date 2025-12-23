using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.DeleteCourseCommand;

public class DeleteCourseCommandRequest : IRequest<DeleteCourseCommandResponse>
{
    public string Id { get; set; }
}

