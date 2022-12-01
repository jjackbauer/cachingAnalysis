public interface ICache<T> where T : class, Imodel
{
    void Add(T input);
    T? Get(long id);
}