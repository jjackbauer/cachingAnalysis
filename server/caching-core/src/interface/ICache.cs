public interface ICache<T> where T : Imodel
{
    Task Add(T input);
    Task<T> Get(long id);
}