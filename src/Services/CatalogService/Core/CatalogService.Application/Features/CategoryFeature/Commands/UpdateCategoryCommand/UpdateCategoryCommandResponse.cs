using BuildingBlocks.Core.Results;

namespace CatalogService.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;

public class UpdateCategoryCommandResponse
{
    public ServiceResult Result { get; set; } = null!;
}