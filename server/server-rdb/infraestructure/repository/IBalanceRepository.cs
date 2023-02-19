using domain_core;

public interface IBalanceRepository
{
    Task Add(AccountBalance input);
    Task AddMany(IEnumerable<AccountBalance> input);
    Task<AccountBalance?> Get(long id);
    Task<long> Commit();
    Task<AccountBalance[]> GetAll();
    Task Erase();
}