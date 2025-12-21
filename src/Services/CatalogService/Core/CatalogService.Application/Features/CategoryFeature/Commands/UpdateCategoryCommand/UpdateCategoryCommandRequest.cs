using CatalogService.Application.Features.CategoryFeature.DTOs;
using MediatR;

namespace CatalogService.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;

public class UpdateCategoryCommandRequest : IRequest<UpdateCategoryCommandResponse>
{
    public UpdateCategoryCommandDto? UpdateCategoryCommandRequestDto { get; set; }
}

