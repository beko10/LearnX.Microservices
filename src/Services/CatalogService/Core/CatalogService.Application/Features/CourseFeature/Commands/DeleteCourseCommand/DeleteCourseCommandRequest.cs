using MediatR;

namespace CatalogService.Application.Features.CourseFeature.Commands.DeleteCourseCommand;

public class DeleteCourseCommandRequest : IRequest<DeleteCourseCommandResponse>
{
    public Guid Id { get; set; }
}

