using Microsoft.AspNetCore.Mvc;
using domain_core;
using System.Diagnostics;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly Config _config;
    private readonly IBalanceRepository _repository;
    public AuthorizationController(IBalanceRepository repository)
    {
        _config = new Config(CacheStrategy.Fifo, 0);
        _repository = repository;
        //Dataset.EraseDatabase(_repository);
        Dataset.PopulateDatabase(_repository, 100000);
    }
    [HttpPost(Name = "PostTransaction")]
    public async Task<PaymentAuthorizationResponse> PostTransaction(PaymentAuthorizationRequest input){
        var a = DateTime.Now;
        var b = DateTime.Now;
        var balance = new AccountBalance{
                Amount = 100,
                UserID = 1
            };

        List<double> cacheTimeElapses = new List<double>();
        List<double> dbTimeElapses = new List<double>();


        List<AccountBalance> buffer = new List<AccountBalance>();

        buffer.Add(balance);
        
        ICache<AccountBalance> cache = new FiFo<AccountBalance>(1000, buffer.ToArray());
    
    long start;
    
    for(int c =0 ; c< 100; c++)
    {
        start = Stopwatch.GetTimestamp();
            cache.Get(balance.UserID); 
        cacheTimeElapses.Add(Stopwatch.GetElapsedTime(start).TotalNanoseconds);
        
    }
        
        

        await _repository.Add(balance);

        await _repository.Commit();

    for(int c = 0; c < 100 ; c++ )
    {
        start = Stopwatch.GetTimestamp();
        await _repository.Get(balance.UserID);

        dbTimeElapses.Add(Stopwatch.GetElapsedTime(start).TotalNanoseconds);
    }
        var dbAvg = dbTimeElapses.Average();
        var cacheAvg = cacheTimeElapses.Average();
        var cacheisFasterBy = dbAvg/cacheAvg;

        var dbTimeElapsesVar = dbTimeElapses.Average(x => Math.Pow(x - dbAvg, 2));

        var cacheTimeElapsesVar = cacheTimeElapses.Average(x => Math.Pow(x - cacheAvg, 2));
    
       PaymentAuthorizationResponse output = new PaymentAuthorizationResponse{
            Approved = true,
            CacheHit = true,
            ArrivalTime = a,
            DepartureTime = b,
            TimeElapsedInNanosseconds = (b - a).Nanoseconds,
        };
        
        return output;
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