using BuildingBlocks.Core.Results;

namespace CatalogService.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;

public class DeleteCategoryCommandResponse
{
    public ServiceResult Result { get; set; } = null!;
}
