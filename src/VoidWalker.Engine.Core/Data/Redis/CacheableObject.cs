using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Core.Data.Redis;

public class CacheableObject<TData>
{
    public static CacheableObject<TData> Create(
        IRedisCacheService redisCacheService, string baseNamespace, Func<Task<TData>> cacheFunction, int defaultCacheTtl = 60
    )
    {
        return new CacheableObject<TData>(redisCacheService, baseNamespace, cacheFunction, defaultCacheTtl);
    }


    private readonly TData _data = default(TData);
    private readonly IRedisCacheService _redisCacheService;
    private readonly int _defaultCacheTtl;
    private readonly string _baseNamespace;
    private readonly Func<Task<TData>> _cacheFunction;


    public CacheableObject(
        IRedisCacheService redisCacheService, string baseNamespace, Func<Task<TData>> cacheFunction, int defaultCacheTtl = 60
    )
    {
        _redisCacheService = redisCacheService;
        _baseNamespace = baseNamespace;
        _defaultCacheTtl = defaultCacheTtl;
        _cacheFunction = cacheFunction;
    }

    private async Task<TData> GetDataValue()
    {
        var cachedObject = await _redisCacheService.GetCacheObjectAsync<TData>(_baseNamespace);

        if (cachedObject != null)
        {
            return cachedObject;
        }

        cachedObject = await _cacheFunction.Invoke();
        await _redisCacheService.SetCacheObjectAsync(_baseNamespace, cachedObject, _defaultCacheTtl);
        return cachedObject;
    }

    public async Task Expire()
    {
        var cachedObject = await _redisCacheService.GetCacheObjectAsync<TData>(_baseNamespace);
        if (cachedObject != null)
            await _redisCacheService.DeleteCacheObjectAsync(_baseNamespace);
    }

    public Task<TData> Get()
    {
        return GetDataValue();
    }
}
