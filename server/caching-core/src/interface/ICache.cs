public interface ICache<T> 
{
    Task Add(T input);
    Task<T> Get(long id);
}