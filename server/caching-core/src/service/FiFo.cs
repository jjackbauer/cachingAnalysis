using System.Collections;

public class FiFo<T> : ICache<T> where T : Imodel
{
    private T[] _cache;
    private readonly long _cacheSize;
    private long head;
    private long tail;
    private readonly Hashtable _hashtable;

    public FiFo(long cacheSize, T[]? preloadItems=null)
    {
        Int32 hashtablesize = (Int32) cacheSize;

        if(cacheSize <= 0){
            throw new Exception("Cache size must be greater than zero");
        }

        if(preloadItems != null && preloadItems.Count() > cacheSize)
        {
            throw new Exception("Cache size must be greater than preloadItems size");
        }

        _cacheSize = cacheSize;
        _cache = preloadItems==null? new T[cacheSize] : preloadItems;

        head = 0; 
        tail = _cache.LongCount();
        
        _hashtable = new Hashtable(hashtablesize);

        if(preloadItems != null)
            foreach(T item in preloadItems)
                _hashtable.Add(item.GetId(), _hashtable.Count);
    }
    public async Task Add(T input){
        if(head == tail)
        

        _cache[pos] = input;

        return;

    }
    public async Task<T> Get(long id){

    }
}