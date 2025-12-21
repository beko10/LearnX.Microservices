using CatalogService.Domain.Entities.Common;

namespace CatalogService.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<Course> Courses { get; set; } = [];
}
