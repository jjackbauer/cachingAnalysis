using Microsoft.AspNetCore.Mvc;
using domain_core;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly Config _config;
    private readonly IBalanceRepository _repository;
    public AuthorizationController(IBalanceRepository repository)
    {
        _config = new Config(CacheStrategy.Fifo, 0);
        _repository = repository;
    }
    [HttpPost(Name = "PostTransaction")]
    public async Task<PaymentAuthorizationResponse> PostTransaction(){
        var a = DateTime.Now;
        var b = DateTime.Now;
        
        
        var balance = new AccountBalance{
                Amount = 100,
                UserID = 1
            };

        await _repository.Add(balance);

        await _repository.Commit();
    
       PaymentAuthorizationResponse output = new PaymentAuthorizationResponse{
            Approved = true,
            CacheHit = true,
            ArrivalTime = a,
            DepartureTime = b,
            TimeElapsedInNanosseconds = (b - a).Nanoseconds,
        };
        
        return output;
    }

    [HttpGet(Name = "Config")]
    public string Config(){
        return _config.ToString();
    }
}