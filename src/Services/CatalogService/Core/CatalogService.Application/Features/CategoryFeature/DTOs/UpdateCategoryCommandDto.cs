using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Application.Features.CategoryFeature.DTOs;
public class UpdateCategoryCommandDto
{
    public string? Id { get; set; } 
    public string Name { get; set; } = null!;

}
