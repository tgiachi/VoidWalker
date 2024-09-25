using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Core.Data.Redis;

public class CacheableIndexObject<TData, TId>
{
    public static CacheableIndexObject<TData, TId> Create(
        IRedisCacheService redisCacheService, string baseNamespace, Func<TId, Task<TData>> cacheFunction,
        Func<TData, Task<TData>> transformFunction = null, int defaultCacheTtl = 60
    )
    {
        return new CacheableIndexObject<TData, TId>(
            redisCacheService,
            baseNamespace,
            cacheFunction,
            transformFunction,
            defaultCacheTtl
        );
    }

    private readonly Func<TId, Task<TData>> _cacheFunction;
    private readonly IRedisCacheService _redisCacheService;
    private readonly Func<TData, Task<TData>> _transformFunction;
    private readonly string _baseNamespace;
    private readonly int _defaultCacheTtl;


    public CacheableIndexObject(
        IRedisCacheService redisCacheService, string baseNamespace,
        Func<TId, Task<TData>> cacheFunction, Func<TData, Task<TData>> transformFunction = null, int defaultCacheTtl = 60
    )
    {
        _baseNamespace = baseNamespace;
        _redisCacheService = redisCacheService;
        _cacheFunction = cacheFunction;
        _defaultCacheTtl = defaultCacheTtl;
        _transformFunction = transformFunction;
    }

    public async Task<TData> Get(TId id)
    {
        var cacheObject = await _redisCacheService.GetCacheObjectAsync<TData>(GetNamespace(id));


        if (cacheObject != null)
        {
            if (_transformFunction != null) cacheObject = await _transformFunction.Invoke(cacheObject);
            return cacheObject;
        }

        cacheObject = await _cacheFunction.Invoke(id);


        await _redisCacheService.SetCacheObjectAsync(GetNamespace(id), cacheObject, _defaultCacheTtl);

        if (_transformFunction != null) cacheObject = await _transformFunction.Invoke(cacheObject);

        return cacheObject;
    }

    public async Task Expire(TId id)
    {
        var cachedObject = await _redisCacheService.GetCacheObjectAsync<TData>(GetNamespace(id));
        if (cachedObject != null)
        {
            await _redisCacheService.DeleteCacheObjectAsync(GetNamespace(id));
        }
    }

    public async Task<TData> ExpireAndGet(TId id)
    {
        await Expire(id);
        return await Get(id);
    }


    public async Task<bool> Exists(TId id)
    {
        var cacheObject = await _redisCacheService.GetCacheObjectAsync<TData>(GetNamespace(id));
        return cacheObject != null;
    }

    public async Task Set(TId id, TData data, int ttl = 60)
    {
        await _redisCacheService.SetCacheObjectAsync(GetNamespace(id), data, ttl);
    }


    private string GetNamespace(TId id) => _baseNamespace + ":" + id;
}
