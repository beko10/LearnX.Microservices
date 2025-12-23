using CatalogService.Domain.Entities.Common;

namespace CatalogService.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Picture { get; set; } 
    public string UserId { get; set; }
    public string CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public Feature Feature { get; set; } = default!;
}




