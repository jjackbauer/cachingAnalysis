using domain_core;

class BalanceOmniRepository 
{
    private readonly IBalanceRepository _repository;
    private readonly Config _config;

    public bool cacheHit { get; set; } = false;

    private readonly ICache<AccountBalance> _cache;

    public BalanceOmniRepository(IBalanceRepository repository, Config config, ICache<AccountBalance> cache)
    {
        _repository = repository;
        _config = config;
        _cache = cache;
    }

    public async Task<AccountBalance?> GetAccountBalance(long id)
    {
        AccountBalance ?output;

        output = _cache.Get(id);

        if(output is null)
        {
            cacheHit = false;
            output = await _repository.Get(id);

            if(output is not null)
                _cache.Add(output);
        }
        else{
            cacheHit = true;
        }

        return output;
    }
 
}
