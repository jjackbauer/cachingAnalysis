using System.Collections;

public class FiFo<T> : ICache<T> where T : class, Imodel 
{
    private T[] _cache;
    private readonly long _cacheSize;
    private long head;
    private long tail;
    private readonly Dictionary<long, long> _dict;

    public FiFo(long cacheSize, T[]? preloadItems=null)
    {
        Int32 hashtablesize = (Int32) cacheSize;

        if(cacheSize <= 0)
            throw new Exception("Cache size must be greater than zero");
        

        if(preloadItems != null && preloadItems.Count() > cacheSize)
            throw new Exception("Cache size must be greater than preloadItems size");

        _cacheSize = cacheSize;
        _cache = preloadItems==null? new T[cacheSize] : preloadItems;

        head = 0; 
        tail = preloadItems is null ? -1 : preloadItems.Length;
        
        _dict = new Dictionary<long, long> (hashtablesize);

        if(preloadItems != null)
            foreach(T item in preloadItems)
                _dict.Add(item.GetId(), _dict.Count);
    }
    public void Add(T input)
    {   T subs;

        if(tail == _cacheSize-1)
            tail = -1;

        tail++;
        subs = _cache[tail];
        _cache[tail]= input;

        if(subs!=null)
            _dict.Remove(subs.GetId());
        
        _dict.Add(input.GetId(), tail);

        return;
    }
    public  T? Get(long id)
    {   long pos;
        
        if(!_dict.TryGetValue(id, out pos))
        {
            return null;
        }

        return _cache[pos];
    }
}
   