using Microsoft.AspNetCore.Mvc;
using domain_core;
using System.Diagnostics;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly Config _config;
    private readonly IBalanceRepository _repository;

    private readonly ICache<AccountBalance> _cache;
    private readonly BalanceOmniRepository _omniRepository;
    public AuthorizationController(IBalanceRepository repository)
    {
        _config = new Config(CacheStrategy.Fifo, 0);
        _repository = repository;
        _cache = new FiFo<AccountBalance>(0);
        _omniRepository = new BalanceOmniRepository(_repository, _config, _cache);
    }
    [HttpPost(Name = "PostTransaction")]
    public async Task<PaymentAuthorizationResponse> PostTransaction(PaymentAuthorizationRequest input){
        throw new NotImplementedException();
    }

    [HttpGet(Name = "Get Server Configuration")]
    public Config Config()
    {
        return _config;
    }


    [HttpPut(Name = "Define Server Configuration")]
    public async Task<Config> ConfigureServer(ServerConfigurationRequest input)
    {
        _config.Configure(input.strategy, input.cacheSize);
        return _config;
    }
}