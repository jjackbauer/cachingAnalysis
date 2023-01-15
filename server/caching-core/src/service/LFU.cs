using System.Collections;
struct LFUNode{
    public long cachePos;
    public long nAccess;
}
public class LFU<T> : ICache<T> where T : class, Imodel 
{
    private T[] _cache;
    private readonly long _cacheSize;
    private long head;
    private long tail;
    private  Dictionary<long, LFUNode> sortedList;

    public LFU(long cacheSize, T[]? preloadItems=null)
    {
        if(cacheSize <= 0)
            throw new Exception("cache size must greater than zero");
            
        _cacheSize = cacheSize;

        if(preloadItems != null && preloadItems.Count() > cacheSize)
            throw new Exception("Cache size must be equal or greater than preloadItems size");

        _cache = preloadItems==null? new T[cacheSize] : preloadItems;
        head = 0; 
        tail = _cache.LongCount() > 0 ? _cache.LongCount(): -1;


        sortedList = new Dictionary<long, LFUNode>((Int32)cacheSize);

        if(preloadItems != null)
            foreach(T item in preloadItems)
                sortedList.Add(item.GetId(),new LFUNode(){
                    cachePos = sortedList.Count,
                    nAccess = 0
                });
    }
    public void Add(T input)
    {  
        sortedList = sortedList.OrderByDescending(x => x.Value.nAccess).ToDictionary(x => x.Key, x=> x.Value);
        var elementToRemove = sortedList.Last();

      _cache[elementToRemove.Value.cachePos] = input;
      sortedList.Remove(elementToRemove.Key);
      sortedList.Add(input.GetId(), new LFUNode(){
        cachePos = elementToRemove.Value.cachePos,
        nAccess= 0
      });
    }
    public  T? Get(long id)
    {   
         LFUNode item;
        
        if(!sortedList.TryGetValue(id, out item))
        {
            return null;
        }

        item.nAccess++;

        sortedList[id] = item;

        sortedList = sortedList.OrderByDescending(x => x.Value.nAccess).ToDictionary(x => x.Key, x => x.Value);

        return _cache[item.cachePos];
    }
}
   