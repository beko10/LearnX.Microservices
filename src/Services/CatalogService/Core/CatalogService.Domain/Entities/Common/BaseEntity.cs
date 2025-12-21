namespace CatalogService.Domain.Entities.Common;

public class BaseEntity
{
    public string Id { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

}
