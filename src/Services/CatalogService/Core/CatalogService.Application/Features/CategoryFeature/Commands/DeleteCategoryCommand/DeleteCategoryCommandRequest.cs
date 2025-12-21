

using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;

public class DeleteCategoryCommandRequest : IRequest<DeleteCategoryCommandResponse>
{
    public Guid Id { get; set; }
}