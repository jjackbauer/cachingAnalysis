public class None<T> : ICache<T> where T : class, Imodel
{
    public void Add(T input)
    {
       return;
    }

    public T? Get(long id)
    {
        return null;
    }
}