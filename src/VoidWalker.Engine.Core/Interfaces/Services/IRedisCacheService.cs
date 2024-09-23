namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface IRedisCacheService : IVoidWalkerService
{
    Task<TEntity> SetCacheObjectAsync<TEntity>(string keyName, TEntity entity, int ttl = 60);

    Task<TEntity> GetCacheObjectAsync<TEntity>(string keyName);

    Task<string> GetCacheStringObjectAsync(string keyName);

    Task<List<TEntity>> GetCacheKeysAsync<TEntity>(string prefix);

    Task<bool> DeleteCacheObjectAsync(string keyName);

}
