using System.Collections;

public class LFU<T> : ICache<T> where T : class, Imodel 
{
    private T[] _cache;
    private readonly long _cacheSize;
    private long head;
    private long tail;
    private readonly Dictionary<long, long> _dict;

    public LFU(long cacheSize, T[]? preloadItems=null)
    {
        
    }
    public void Add(T input)
    {  
    }
    public  T? Get(long id)
    {   
        
    }
}
   