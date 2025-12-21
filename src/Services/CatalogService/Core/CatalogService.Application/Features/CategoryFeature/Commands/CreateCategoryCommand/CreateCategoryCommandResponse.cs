using BuildingBlocks.Core.Results;

namespace CatalogService.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;

public class CreateCategoryCommandResponse
{
    public ServiceResult Result { get; set; } = null!;
}
