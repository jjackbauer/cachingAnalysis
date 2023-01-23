using Microsoft.AspNetCore.Mvc;
using domain_core;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly Config _config;
    public AuthorizationController()
    {
        _config = new Config(CacheStrategy.Fifo, 0);
    }
    [HttpPost(Name = "PostTransaction")]
    public PaymentAuthorizationResponse PostTransaction(){
        var a = DateTime.Now;
        var b = DateTime.Now;

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