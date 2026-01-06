using System.ComponentModel.DataAnnotations;

namespace DiscountService.API.Common.MongoDB;

public class MongoOptions
{
    public const string SectionName = "MongoOptions";

    [Required]
    public string ConnectionString { get; set; } = default!;

    [Required]
    public string DatabaseName { get; set; } = default!;
}
