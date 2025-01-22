using System.Text.Json;
using StackExchange.Redis;

namespace ChatAI.Persistence.Cache;

public class RedisCache
{
    private readonly ConnectionMultiplexer _connectionMultiplexer;
    private IDatabase Db=> _connectionMultiplexer.GetDatabase();

    public RedisCache(ConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }
    
    public async Task<T> GetAsync<T>(string key)
    {
        var value = await Db.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value) : default;
    }
    
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        await Db.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
    }
}