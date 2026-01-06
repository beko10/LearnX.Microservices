using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BasketService.API.Common.Redis;

public class RedisService : IRedisService
{
    private readonly IDatabase _database;
    private readonly RedisOptions _options;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public RedisService(IConnectionMultiplexer connectionMultiplexer, IOptions<RedisOptions> options)
    {
        _database = connectionMultiplexer.GetDatabase();
        _options = options.Value;
    }

    private string GetKey(string key) => $"{_options.InstanceName}{key}";

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _database.StringGetAsync(GetKey(key));
        
        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value.ToString(), JsonOptions);
    }

    public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var serializedValue = JsonSerializer.Serialize(value, JsonOptions);
        var defaultExpiry = TimeSpan.FromHours(_options.DefaultExpiryHours);
        
        return await _database.StringSetAsync(
            GetKey(key), 
            serializedValue, 
            expiry ?? defaultExpiry
        );
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _database.KeyDeleteAsync(GetKey(key));
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _database.KeyExistsAsync(GetKey(key));
    }
}
