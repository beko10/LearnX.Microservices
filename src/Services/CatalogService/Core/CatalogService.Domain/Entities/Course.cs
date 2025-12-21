using CatalogService.Domain.Entities.Common;

namespace CatalogService.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Picture { get; set; } 
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public Feature Feature { get; set; } = default!;
}




