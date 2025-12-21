using CatalogService.Application.Features.CategoryFeature.DTOs;
using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;

public class CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>
{
    public CreateCategoryCommandDto? CreateCategoryCommandRequestDto { get; set; }
}
