public class Only<T> : ICache<T> where T : class, Imodel
{   
    private T[] _cache;
    private readonly long _cacheSize;

    public Only(T[] preloadItems,long cacheSize)
    {
        if(preloadItems is null)
            throw new Exception("preload items mustn't be null");

        if(preloadItems.Count() != cacheSize)
            throw new Exception($"preload items count:{preloadItems.Count()} must have the cache size: {cacheSize}");
        
        _cache = preloadItems;
        _cacheSize = cacheSize;
    }

    public void Add(T input)
    {
        return;
    }

    public T? Get(long id)
    {
       if(id > _cacheSize)
        return null;

        return _cache[id-1];
    }
}