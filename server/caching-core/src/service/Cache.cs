using System.Collections;

public class Cache<T> : ICache<T>
{
    private T[] _cache;
    private readonly long _cacheSize;
    private readonly IReplacementMethod _replacer;
    private readonly Hashtable _hashtable;

    public Cache(IReplacementMethod replacementMethod,long cacheSize)
    {
        if(cacheSize <= 0){
            throw new Exception("Cache size must be greater than zero");
        }

        _replacer = replacementMethod;
        _cacheSize = cacheSize;
        _cache = new T[cacheSize];
        Int32 hashtablesize = (Int32) cacheSize;
        _hashtable = new Hashtable(hashtablesize);
    }
    public async Task Add(T input){
        long pos = await _replacer.GetIdToReplace();

        if(pos < 0){
            throw new Exception("Invalid position");
        }

        _cache[pos] = input;

        return;

    }
    public async Task<T> Get(long id){

    }
}