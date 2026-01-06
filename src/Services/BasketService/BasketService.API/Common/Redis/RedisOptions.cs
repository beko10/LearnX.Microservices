using System.ComponentModel.DataAnnotations;

namespace BasketService.API.Common.Redis;

public class RedisOptions
{
    public const string SectionName = "RedisOptions";
    
    [Required]
    public string ConnectionString { get; set; } = default!;
    
    public string InstanceName { get; set; } = "BasketService_";
    
    public int DefaultExpiryHours { get; set; } = 24;
}
