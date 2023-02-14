using Microsoft.AspNetCore.Mvc;
using domain_core;
using System.Diagnostics;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private static Config _config;
    private readonly IBalanceRepository _repository;

    private  static ICache<AccountBalance> _cache;
    private BalanceOmniRepository _omniRepository;
    public AuthorizationController(IBalanceRepository repository)
    {
        if (_config is null )
            _config = new Config(CacheStrategy.Fifo, 3);
        
        _repository = repository;
        if (_cache is null)
            _cache = new FiFo<AccountBalance>(3);
        
        _omniRepository = new BalanceOmniRepository(_repository, _config, _cache);
    }
    [HttpPost(Name = "PostTransaction")]
    public async Task<ActionResult> PostTransaction(PaymentAuthorizationRequest input){
        
    PaymentAuthorizationResponse output = new PaymentAuthorizationResponse();

    output.ArrivalTime = DateTime.Now;


       var balance =  await _omniRepository.GetAccountBalance(input.UserID);

       if (balance is null)
        return NotFound($"balance for userid = {input.UserID} not found");

        output.Approved = balance.Amount >= input.Value;
        output.CacheHit = _omniRepository.cacheHit;
        output.DepartureTime = DateTime.Now;

        return Ok(output);
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

        _cache = input.strategy == CacheStrategy.Fifo ?
                                        new FiFo<AccountBalance>(input.cacheSize)
                                        :
                                        new LFU<AccountBalance>(input.cacheSize);

        _omniRepository = new BalanceOmniRepository(
                                _repository,
                                _config,
                                _cache
            );

        
        GC.Collect();

        GC.WaitForFullGCComplete();

        return _config;
    }
}